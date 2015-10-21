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
using System.Linq.Expressions;
using System.Reflection;

namespace UnitTestGenerator
{
    /// <summary>
    /// Provides methods for building <see cref="Expression"/>.
    /// </summary>
    public interface IExpressionBuilder
    {
        /// <summary>
        /// Creates an <see cref="Expression"/> which represents a method or constructor argument.
        /// </summary>
        /// <param name="type">The type of object to create the expression.</param>
        /// <returns>The created expression.</returns>
        Expression CreateArgumentExpression(Type type);

        /// <summary>
        /// Creates an <see cref="Expression"/> which creates the supplied type.
        /// </summary>
        /// <param name="type">The type of object to create the expression.</param>
        /// <returns>The created expression.</returns>
        Expression CreateInstanceCreationExpression(Type type);

        /// <summary>
        /// Produces string representation of <see cref="Expression"/>. 
        /// </summary>
        /// <param name="expression">The expression to convert.</param>
        /// <returns>Returns a string representation of <see cref="Expression"/>.</returns>
        string ExpressionToString(Expression expression);

        /// <summary>
        /// Produces an enumeration of <see cref="Expression"/> from an enumeration of <see cref="ParameterInfo"/>.
        /// </summary>
        /// <param name="parameters">The enumeration of <see cref="ParameterInfo"/> from which is produced the enumeration of <see cref="Expression"/>.</param>
        /// <param name="expicitCast">True, when the <paramref name="parameterDestinedAsNull"/> should be cast explicitly.</param>
        /// <param name="parameterDestinedAsNull">Parameter destined to be null</param>
        /// <returns>The enumeration of <see cref="Expression"/> produced from a collection of <see cref="ParameterInfo"/>.</returns>
        IEnumerable<Expression> CreateArgumentExpressions(IEnumerable<ParameterInfo> parameters, bool expicitCast = false, ParameterInfo parameterDestinedAsNull = null);

        /// <summary>
        /// Produces an <see cref="Expression"/> which represents an instance creation of passed <paramref name="targetType"/> and assignment to a local variable.
        /// </summary>
        /// <param name="targetType">The type which creation should be built to <see cref="Expression"/>.</param>
        /// <param name="referencedVariableExpression">When returns true, <paramref name="referencedVariableExpression"/> contains expression which refers to the newly created instance, otherwise null.</param>
        /// <returns>The <see cref="Expression"/> containing the assignment and creation of type.</returns>
        BinaryExpression BuildInstanceCreationWithAssigmentExpression(Type targetType, out ParameterExpression referencedVariableExpression);
    }
}
