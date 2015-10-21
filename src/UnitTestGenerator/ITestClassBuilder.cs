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
using System.Collections.Generic;

namespace UnitTestGenerator
{
    /// <summary>
    /// Builds unit test classes
    /// </summary>
    public interface ITestClassBuilder
    {
        /// <summary>
        /// Builds a collection of test classes.
        /// </summary>
        /// <returns>Collection of built test classes.</returns>
        IEnumerable<TestClass> BuildTestClasses();
    }
}
