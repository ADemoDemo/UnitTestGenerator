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
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestGenerator.CodeGeneration.Generators
{
    /// <summary>
    /// Represent a request object which contains informations for generating source code which calls a constructor.
    /// </summary>
    public class ConstructorSourceCodeGenerationRequest : SourceCodeGenerationRequest
    {
        private readonly ConstructorInfo constructor;
        private readonly bool hasMultipleConstructors;

        /// <summary>
        /// Initializes a new instance of the ConstructorSourceCodeGenerationRequest class.
        /// </summary>
        /// <param name="constructor"></param>
        /// <param name="nullArgumentNeedsExplicitCast"></param>
        /// <param name="hasMultipleConstructors"></param>
        /// <param name="targetParameter"></param>
        public ConstructorSourceCodeGenerationRequest(ConstructorInfo constructor, bool nullArgumentNeedsExplicitCast, bool hasMultipleConstructors, ParameterInfo targetParameter = null)
            : base(nullArgumentNeedsExplicitCast, targetParameter)
        {
            Check.NotNull(constructor, "constructor");
            this.constructor = constructor;
            this.hasMultipleConstructors = hasMultipleConstructors;
        }

        /// <summary>
        /// Gets the constructor descriptor.
        /// </summary>
        public ConstructorInfo Constructor
        {
            get
            {
                return constructor;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the type where the <see cref="P:Constructor"/> belongs to have multiple constructors.
        /// </summary>
        public bool HasMultipleConstructors
        {
            get
            {
                return hasMultipleConstructors;
            }
        }
    }
}
