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
    /// Represent meta-data for tested method.
    /// </summary>
    public class MethodMetadata
    {
        private readonly MethodInfo method;
        private readonly bool nullArgumentNeedsExplicitCast;

        /// <summary>
        /// Initializes a new instance of the MethodMetadata class.
        /// </summary>
        /// <param name="method">Method descriptor.</param>
        /// <param name="nullArgumentNeedsExplicitCast">Value indicating whether passed method parameters need explicit cast.</param>
        public MethodMetadata(MethodInfo method, bool nullArgumentNeedsExplicitCast)
        {
            Check.NotNull(method, "method");
            this.method = method;
            this.nullArgumentNeedsExplicitCast = nullArgumentNeedsExplicitCast;
        }

        /// <summary>
        /// Gets the method's descriptor.
        /// </summary>
        public MethodInfo Method
        {
            get
            {
                return method;
            }
        }

        /// <summary>
        /// Gets a value indicating whether passed method parameters need explicit cast.
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
