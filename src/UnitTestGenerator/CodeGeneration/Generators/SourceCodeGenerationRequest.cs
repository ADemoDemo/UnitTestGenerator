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
    /// Represents a base request object for generating source code.
    /// </summary>
    public abstract class SourceCodeGenerationRequest
    {
        private readonly ParameterInfo targetParameter;
        private readonly bool nullArgumentNeedsExplicitCast;

        protected SourceCodeGenerationRequest(bool nullArgumentNeedsExplicitCast, ParameterInfo targetParameter = null)
        {
            this.targetParameter = targetParameter;
            this.nullArgumentNeedsExplicitCast = nullArgumentNeedsExplicitCast;
        }

        /// <summary>
        /// Gets the parameter destined to be null
        /// </summary>
        public ParameterInfo ParameterDestinedAsNull
        {
            get
            {
                return targetParameter;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the <see cref="P:ParameterDestinedAsNull"/> needs explicit cast.
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
