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

namespace UnitTestGenerator
{
    /// <summary>
    /// Provides an interface for validating variables for name.
    /// </summary>
    public interface IIdentifierValidator
    {
        /// <summary>
        /// Returns true, when the passed variable does not collide with any keyword, otherwise false.
        /// </summary>
        /// <param name="varName">The name of variable.</param>
        /// <returns>Returns true, when the passed variable does not collide with any keyword, otherwise false.</returns>
        bool IsValidIdentifier(string varName);
    }
}
