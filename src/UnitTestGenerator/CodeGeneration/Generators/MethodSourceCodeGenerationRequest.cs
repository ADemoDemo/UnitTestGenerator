﻿/* ****************************************************************************
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

using System.Reflection;

namespace UnitTestGenerator.CodeGeneration.Generators
{
    /// <summary>
    /// Represent a request object which contains informations for generating source code which calls a method.
    /// </summary>
    public class MethodSourceCodeGenerationRequest : SourceCodeGenerationRequest
    {
        private readonly MethodInfo method;
        readonly bool hasOverloadWithConflictingParameterName;

        /// <summary>
        /// Initializes a new instance of the MethodSourceCodeGenerationRequest class.
        /// </summary>
        /// <param name="method">The method descriptor.</param>
        /// <param name="nullArgumentNeedsExplicitCast">The value indicating whether the <paramref name="targetParameter"/> needs explicit cast.</param>
        /// <param name="targetParameter">The parameter which is tested.</param>
        /// <param name="hasOverloadWithConflictingParameterName">Represents a value whether the passed method has overloads with conflicting parameter name.</param>
        public MethodSourceCodeGenerationRequest(MethodInfo method, 
            bool nullArgumentNeedsExplicitCast = false, 
            ParameterInfo targetParameter = null,
            bool hasOverloadWithConflictingParameterName = false)
            : base(nullArgumentNeedsExplicitCast, targetParameter)
        {
            Check.NotNull(method, "method");
            this.method = method;
            this.hasOverloadWithConflictingParameterName = hasOverloadWithConflictingParameterName;
        }

        /// <summary>
        /// Gets the method descriptor.
        /// </summary>
        public MethodInfo Method
        {
            get
            {
                return method;
            }
        }

        /// <summary>
        /// Gets a value representing whether the passed method has overloads with conflicting parameter name.
        /// </summary>
        public bool HasOverloadWithConflictingParameterName
        {
            get
            {
                return hasOverloadWithConflictingParameterName;
            }
        }
    }
}