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

namespace UnitTestGenerator.CodeGeneration
{
    /// <summary>
    /// Provides an interface for creating Expression.
    /// </summary>
    public interface IValueExpressionProvider
    {
        /// <summary>
        /// Returns an expression that contains creation of passed type.
        /// </summary>
        /// <param name="forType">The type which creation should be contained in expression.</param>
        /// <returns>The expression that contains creation of passed type.</returns>
        Expression CreateValueExpression(Type forType);
    }
}
