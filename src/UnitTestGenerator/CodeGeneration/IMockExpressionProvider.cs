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
    /// Provides an interface for creation of complex types.
    /// </summary>
    public interface IMockExpressionProvider
    {
        /// <summary>
        /// Creates an expression which retrieves and instance of <paramref name="forType"/>.
        /// </summary>
        /// <param name="forType">The type to mock.</param>
        /// <returns>The expression which retrieves and instance of <paramref name="forType"/>.</returns>
        Expression CreateMockExpression(Type forType);
    }
}
