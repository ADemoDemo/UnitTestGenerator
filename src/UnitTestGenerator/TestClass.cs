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
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace UnitTestGenerator
{
    /// <summary>
    /// Represent an unit test class with methods.
    /// </summary>
    [DebuggerDisplay("TestClass: {TestedType.Name}")]
    public class TestClass
    {
        private readonly Type testedType;
        private IEnumerable<TestMethod> methods;

        /// <summary>
        /// Initializes a new instance of the TestMethod class.
        /// </summary>
        /// <param name="testedType"></param>
        /// <param name="methods"></param>
        public TestClass(Type testedType, IEnumerable<TestMethod> methods)
        {
            Check.NotNull(testedType, "testedType");
            Check.NotNull(methods, "methods");

            this.testedType = testedType;
            this.methods = methods;
        }

        public Type TestedType
        {
            get { return testedType; }
        }

        public IEnumerable<TestMethod> Methods
        {
            get { return methods; }
        }
    }
}
