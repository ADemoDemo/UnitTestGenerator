/* ****************************************************************************
 * Copyright 2015 Peter Csikós
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 *
 * ***************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace UnitTestGenerator.CodeGeneration
{
    /// <summary>
    /// Provides methods for building <see cref="Expression"/>.
    /// </summary>
    public class ExpressionBuilder : IExpressionBuilder
    {
        private readonly ITestMethodValueProvider testMethodValueProvider;
        private readonly IMockExpressionProvider mockProvider;
        private readonly IValueExpressionProvider valueExpressionProvider;
        readonly IIdentifierValidator identifierValidator;

        /// <summary>
        /// Initializes a new instance of the ExpressionBuilder class.
        /// </summary>
        /// <param name="testMethodValueProvider">External <see cref="Expression"/> provider.</param>
        /// <param name="mockProvider"></param>
        /// <param name="valueExpressionProvider"></param>
        public ExpressionBuilder(ITestMethodValueProvider testMethodValueProvider,
            IMockExpressionProvider mockProvider,
            IValueExpressionProvider valueExpressionProvider,
            IIdentifierValidator identifierValidator)
        {
            
            Check.NotNull(testMethodValueProvider, "testMethodValueProvider");
            Check.NotNull(mockProvider, "mockProvider");
            Check.NotNull(valueExpressionProvider, "valueExpressionProvider");
            Check.NotNull(identifierValidator, nameof(identifierValidator));

            this.testMethodValueProvider = testMethodValueProvider;
            this.valueExpressionProvider = valueExpressionProvider;
            this.mockProvider = mockProvider;
            this.identifierValidator = identifierValidator;
        }

        /// <summary>
        /// Creates an <see cref="Expression"/> which represents a method or constructor argument.
        /// </summary>
        /// <param name="type">The type of object to create the expression.</param>
        /// <returns>The created expression.</returns>
        public Expression CreateArgumentExpression(Type type)
        {
            Check.NotNull(type, nameof(type));
            if (type.IsGenericTypeDefinition)
            {
                throw new ArgumentException($"Generic type {type.Name} is not supported.");
            }
            if (type.IsByRef)
            {
                return CreateArgumentExpression(type.GetElementType());
            }
            Expression expression;
            if (testMethodValueProvider.HasExpressionForArgument(type, out expression))
            {
                return expression;
            }
            if (type == typeof(string))
            {
                return CreateValueType(typeof(string));
            }
            if (type.IsArray || (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(IEnumerable<>)))
            {
                return CreateArray(type.IsArray ? type.GetElementType() : type.GetGenericArguments().First());
            }
            if (type.IsValueType)
            {
                return CreateValueType(type);
            }
            if (type.IsInterface && type.IsGenericType && TryCreateGenericCollection(type, out expression))
            {
                return expression;
            }
            if (type.Name.StartsWith("Func`", StringComparison.InvariantCulture))
            {
                return CreateFunc(type);
            }

            var ctors = type.GetConstructors(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.CreateInstance | BindingFlags.Instance);
            var defaultCtor = ctors.FirstOrDefault(x => x.IsPublic && x.GetParameters().Count() == 0);
            if (defaultCtor != null)
            {
                return Expression.New(defaultCtor);
            }
            var publicCtor = ctors.Where(x => x.IsPublic).OrderBy(x => x.GetParameters().Length).FirstOrDefault();
            if (publicCtor == null)//no public ctor
            {
                return CreateInstanceCreationExpression(type);
            }

            return CreateInstance(publicCtor);
        }

        /// <summary>
        /// Creates an <see cref="Expression"/> which creates the supplied type. If no public constructor is found a proxy class is created if possible.
        /// </summary>
        /// <param name="type">The type of object to create the expression.</param>
        /// <returns>The created expression.</returns>
        public Expression CreateInstanceCreationExpression(Type type)
        {
            Check.NotNull(type, "type");
            if (HasPublicConstructor(type))
            {
                return Expression.New(type);
            }
            return mockProvider.CreateMockExpression(type);
        }

        /// <summary>
        /// Produces string representation of <see cref="Expression"/>. 
        /// </summary>
        /// <param name="expression">The expression to convert.</param>
        /// <returns>Returns a string representation of <see cref="Expression"/>.</returns>
        public string ExpressionToString(Expression expression)
        {
            return ExpressionStringBuilder.ExpressionToString(expression);
        }

        /// <summary>
        /// Produces an enumeration of <see cref="Expression"/> from an enumeration of <see cref="ParameterInfo"/>.
        /// </summary>
        /// <param name="parameters">The enumeration of <see cref="ParameterInfo"/> from which is produced the enumeration of <see cref="Expression"/>.</param>
        /// <param name="expicitCast">True, when the <paramref name="parameterDesignatedAsNull"/> should be cast explicitly.</param>
        /// <param name="parameterDesignatedAsNull">Parameter designated to be null</param>
        /// <returns>The enumeration of <see cref="Expression"/> produced from a collection of <see cref="ParameterInfo"/>.</returns>
        public IEnumerable<Expression> CreateArgumentExpressions(IEnumerable<ParameterInfo> parameters, bool expicitCast = false, ParameterInfo parameterDestinedAsNull = null)
        {
            Check.NotEmpty(parameters, nameof(parameters));

            var localParameters = parameters.ToArray();
            var arguments = new Expression[localParameters.Length];
            for (int i = 0; i < localParameters.Length; i++)
            {
                if (localParameters[i] == parameterDestinedAsNull)
                {
                    arguments[i] = CreateNullExpressionForTestedArgument(localParameters[i], expicitCast);
                }
                else
                {
                    arguments[i] = CreateArgumentExpression(localParameters[i].ParameterType);
                }
            }

            return arguments;
        }

        /// <summary>
        /// Produces an <see cref="Expression"/> which represents an instance creation of passed <paramref name="targetType"/> and assignment to a local variable.
        /// </summary>
        /// <param name="targetType">The type which creation should be built to <see cref="Expression"/>.</param>
        /// <param name="referencedVariableExpression">When returns true, <paramref name="referencedVariableExpression"/> contains expression which refers to the newly created instance, otherwise null.</param>
        /// <returns>The <see cref="Expression"/> containing the assignment and creation of type.</returns>
        public BinaryExpression BuildInstanceCreationWithAssigmentExpression(Type targetType, out ParameterExpression referencedVariableExpression)
        {
            var createInstanceExpr = CreateInstanceCreationExpression(targetType);
            var varName = targetType.Name.ToCamelCase();
            if (!identifierValidator.IsValidIdentifier(varName))
            {
                varName = "@" + varName;
            }
            referencedVariableExpression = Expression.Variable(targetType, varName);
            var assignExpr = Expression.Assign(referencedVariableExpression, createInstanceExpr);
            return assignExpr;
        }

        private Expression CreateNullExpressionForTestedArgument(ParameterInfo parameter, bool explicitCast)
        {
            var constant = Expression.Constant(null, parameter.ParameterType);
            if (!explicitCast)
            {
                return constant;
            }
            return Expression.Convert(constant, parameter.ParameterType);
        }

        private static bool HasPublicConstructor(Type type)
        {
            return type.GetConstructors().Any(x => x.IsPublic && x.GetParameters().Length == 0);
        }

        private bool TryCreateGenericCollection(Type type, out Expression expression)
        {
            var generic = type.GetGenericTypeDefinition();
            var arguments = type.GetGenericArguments();
            if (arguments.Length == 1 && (generic == typeof(IList<>) || generic == typeof(ICollection<>) || generic == typeof(IEnumerable<>)))
            {
                expression = CreateList(arguments.Single());
                return true;
            }
            else if (arguments.Length == 2 && generic == typeof(IDictionary<,>))
            {
                expression = CreateDictionary(arguments);
                return true;
            }
            expression = null;
            return false;
        }

        private static Expression CreateDictionary(Type[] arguments)
        {
            Expression expression;
            var dictType = typeof(Dictionary<,>).MakeGenericType(arguments);
            expression = Expression.New(dictType);
            return expression;
        }

        private Expression CreateList(Type argument)
        {
            Expression expression;
            var listType = typeof(List<>).MakeGenericType(argument);
            var ctor = listType.GetConstructors().Single(x => x.GetParameters().Any(p => p.ParameterType == typeof(IEnumerable<>).MakeGenericType(argument)));
            expression = Expression.New(ctor, CreateArray(argument));
            return expression;
        }

        private Expression CreateFunc(Type t)
        {
            var arguments = t.GetGenericArguments();
            var func = Expression.Lambda(t, CreateArgumentExpression(arguments.Last()), arguments.Take(arguments.Length - 1).Select(x => Expression.Parameter(x)));
            return func;
        }

        private Expression CreateInstance(ConstructorInfo publicCtor)
        {
            var parameters = publicCtor.GetParameters();
            var arguments = new Expression[parameters.Length];
            foreach (var p in parameters)
            {
                arguments[p.Position] = CreateArgumentExpression(p.ParameterType);
            }
            return Expression.New(publicCtor, arguments);
        }

        private Expression CreateValueType(Type t)
        {
            return valueExpressionProvider.CreateValueExpression(t);
        }

        private Expression CreateArray(Type t)
        {
            var arrayItems = new Expression[] { CreateArgumentExpression(t), CreateArgumentExpression(t) };
            return Expression.NewArrayInit(t, arrayItems);
        }
    }
}
