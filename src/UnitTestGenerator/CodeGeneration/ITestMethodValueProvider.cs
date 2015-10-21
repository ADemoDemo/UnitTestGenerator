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
using System.Linq.Expressions;

namespace UnitTestGenerator
{
    /// <summary>
    /// Provides methods which provides source code construction elements (like variable names, built expressions, etc.) in a process of building source code.
    /// </summary>
    public interface ITestMethodValueProvider
    {
        /// <summary>
        /// Returns true when <paramref name="testedInstanceType"/> is registered and therefore instance creation expression should not be created, otherwise returns false.
        /// </summary>
        /// <param name="testedInstanceType">The tested type.</param>
        /// <param name="variableName">When returns true, contains the name of variable which was registered, otherwise is set to null.</param>
        /// <returns>True when <paramref name="testedInstanceType"/> is registered, otherwise false.</returns>
        bool HasVariableForTestedType(Type testedInstanceType, out string variableName);

        /// <summary>
        /// Returns true when the registered expression for an argument type should be used instead of generated expression. Registered expression is returned as <paramref name="argumentExpression"/>.
        /// </summary>
        /// <param name="argumentType">The registered argument type.</param>
        /// <param name="argumentExpression">When returns true, contains the registered expression, otherwise the value is null.</param>
        /// <returns>True when the expression of a registered argument type should be used instead of generated expression.</returns>
        bool HasExpressionForArgument(Type argumentType, out Expression argumentExpression);
    }
}
