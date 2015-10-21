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
using System.Diagnostics;
using System.Linq;
using UnitTestGenerator.UnitTestGeneration;

namespace UnitTestGenerator
{
    /// <summary>
    /// Builds unit test classes based on provided types.
    /// </summary>
    public class TestClassBuilder : ITestClassBuilder
    {
        private readonly IAssemblyTraverser traverser;
        private readonly IEnumerable<ITestMethodGenerator> testGenerators;

        /// <summary>
        /// Initializes a new instance of the TestClassBuilder class.
        /// </summary>
        /// <param name="traverser">Configured <see cref="T:IAssemblyTraverser" /> which provides desired types.</param>
        /// <param name="testGenerators">Collection of test method generators passed as <see cref="T:System.Collections.Generic.IEnumerable&lt;ITestMethodGenerator&gt;"/>.</param>
        public TestClassBuilder(IAssemblyTraverser traverser, IEnumerable<ITestMethodGenerator> testGenerators)
        {
            Check.NotNull(traverser, nameof(traverser));
            Check.NotEmpty(testGenerators, nameof(testGenerators));

            this.traverser = traverser;
            this.testGenerators = testGenerators.ToArray();
        }

        /// <summary>
        /// Invokes every passed <see cref="ITestMethodGenerator" /> on types provided by <see cref="IAssemblyTraverser" />.
        /// </summary>
        /// <returns>Collection of built test classes.</returns>
        public IEnumerable<TestClass> BuildTestClasses()
        {
            var types = new List<TestClass>();
            foreach (var type in traverser.GetTypes())
            {
                var tests = BuildTestsForType(type);
                if (tests.Any())
                {
                    types.Add(new TestClass(type, tests));
                }
            }
            return types;
        }

        private IEnumerable<TestMethod> BuildTestsForType(Type type)
        {
            Debug.Assert(type != null);
            

            var tests = testGenerators.Select(generator => generator.GenerateTestMethods(type))
                .SelectMany(t => t).ToArray();
            return tests;
        }
    }
}
