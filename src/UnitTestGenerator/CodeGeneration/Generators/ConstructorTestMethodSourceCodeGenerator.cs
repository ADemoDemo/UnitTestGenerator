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
using System.Text;
using System.Threading.Tasks;

namespace UnitTestGenerator.CodeGeneration.Generators
{
    /// <summary>
    /// Provides a base class which creates the basic structure of a test method source code generation for a constructor.
    /// </summary>
    public abstract class ConstructorTestMethodSourceCodeGenerator : AbstractTestMethodSourceCodeGenerator<ConstructorSourceCodeGenerationRequest>
    {
        readonly IExpressionBuilder expressionBuilder;

        protected ConstructorTestMethodSourceCodeGenerator(IExpressionBuilder expressionBuilder)
        {
            Check.NotNull(expressionBuilder, nameof(expressionBuilder));
            this.expressionBuilder = expressionBuilder;
        }

        protected virtual Expression CreateCtorExpression(ConstructorInfo ctor, ParameterInfo targetParameter, bool explicitCast)//, Expression parameterExpression = null)
        {
            Check.NotNull(ctor, nameof(ctor));
            var parameters = ctor.GetParameters();
            Expression[] arguments = expressionBuilder.CreateArgumentExpressions(parameters, explicitCast, targetParameter).ToArray();
            return Expression.New(ctor, arguments);
        }

        protected override void BuildActSourceCode(ConstructorSourceCodeGenerationRequest request)
        {
            var createInstanceExpr = CreateCtorExpression(request.Constructor, request.ParameterDestinedAsNull, request.NullArgumentNeedsExplicitCast);

            var varExpr = Expression.Variable(request.Constructor.DeclaringType, "instance");
            var assignExpr = Expression.Assign(varExpr, createInstanceExpr);

            Append("var ");
            Append(expressionBuilder.ExpressionToString(assignExpr).RemoveParantheses());
            AppendLine(";");
        }

        protected static string BuildConstructorName(ConstructorSourceCodeGenerationRequest request)
        {
            if (request.HasMultipleConstructors)
            {
                return "_" + string.Join("_", request.Constructor.GetParameters().Select(x => GetSafeTypeName(x.ParameterType)).ToArray());
            }
            return "";
        }

        private static string GetSafeTypeName(Type type)
        {
            if (type.IsGenericType)
            {
                return type.Name.Substring(0, type.Name.IndexOf('`'));
            }
            return type.Name;
        }
    }
}
