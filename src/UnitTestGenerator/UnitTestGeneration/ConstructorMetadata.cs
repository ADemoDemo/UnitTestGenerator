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

using System.Reflection;

namespace UnitTestGenerator.UnitTestGeneration
{
    /// <summary>
    /// Represent meta-data for tested constructor.
    /// </summary>
    public class ConstructorMetadata
    {
        private readonly ConstructorInfo ctor;
        private readonly bool hasMultipleConstructors;
        private readonly bool nullArgumentNeedsExplicitCast;

        /// <summary>
        /// Initializes a new instance of the ConstructorMetadata class.
        /// </summary>
        /// <param name="constructor">Constructor's descriptor</param>
        /// <param name="nullArgumentNeedsExplicitCast">Value indicating whether passed constructor parameters need explicit cast.</param>
        /// <param name="hasMultipleConstructors">Gets a value indicating whether the type where the passed constructor belongs to have multiple constructors with the same number of parameter.</param>
        public ConstructorMetadata(ConstructorInfo constructor, 
            bool nullArgumentNeedsExplicitCast,
            bool hasMultipleConstructors)
        {
            Check.NotNull(constructor, nameof(constructor));

            this.ctor = constructor;
            this.nullArgumentNeedsExplicitCast = nullArgumentNeedsExplicitCast;
            this.hasMultipleConstructors = hasMultipleConstructors;
        }

        /// <summary>
        /// Get the constructor descriptor.
        /// </summary>
        public ConstructorInfo Constructor
        {
            get
            {
                return ctor;
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

        /// <summary>
        /// Gets a value indicating whether passed constructor parameters need explicit cast.
        /// </summary>
        public bool NullArgumentNeedsExplicitCast
        {
            get
            {
                return nullArgumentNeedsExplicitCast;
            }
        }
    }
}
