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

using Microsoft.CSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestGenerator
{
    /// <summary>
    /// Provides validation about collisions with C# keywords
    /// </summary>
    public class CSharpIdentifierValidator : IIdentifierValidator, IDisposable
    {
        private readonly CSharpCodeProvider codeProvider = new CSharpCodeProvider();

        /// <summary>
        /// Returns true, when the passed variable does not collide with C# keyword, otherwise false.
        /// </summary>
        /// <param name="varName">The name of variable.</param>
        /// <returns>Returns true, when the passed variable does not collide with C# keyword, otherwise false.</returns>
        public bool IsValidIdentifier(string varName)
        {
            Check.NotNull(varName, "varName");
            return codeProvider.IsValidIdentifier(varName);
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                disposedValue = true;

                if (disposing)
                {
                    codeProvider.Dispose();
                }
            }
        }
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
        }
        #endregion
    }
}
