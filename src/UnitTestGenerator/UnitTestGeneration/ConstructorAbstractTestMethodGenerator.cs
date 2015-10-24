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
using System.Reflection;
using System.Linq;


namespace UnitTestGenerator.UnitTestGeneration
{
    /// <summary>
    /// Provides a base class for generation of collection of <see cref="TestMethod"/> based on passed <see cref="Type"/> for constructors. 
    /// </summary>
    public abstract class ConstructorAbstractTestMethodGenerator : ITestMethodGenerator
    {
        protected ConstructorAbstractTestMethodGenerator()
        {
        }

        /// <summary>
        /// Generates a collection of <see cref="TestMethod"/>.
        /// </summary>
        /// <param name="typeContext">The context for which the methods are generated.</param>
        /// <returns>Collection of <see cref="TestMethod"/>.</returns>
        public IEnumerable<TestMethod> GenerateTestMethods(TypeContext typeContext)
        {
            var tests = new List<TestMethod>();
            var ctorsToTest = GetAccessibleContructors(typeContext.TargetType, typeContext.InternalsVisible);
            var explicitMethodParameters = GetCtorsWithExplicitParameterCast(ctorsToTest);
            var hasMultipleConstructors = HasMultipleParametrizedConstructors(ctorsToTest);

            foreach (var ctor in ctorsToTest)
            {
                var metadata = new ConstructorMetadata(ctor, explicitMethodParameters.Contains(ctor), hasMultipleConstructors);
                var testMethods = GenerateTestMethodsFor(metadata);
                tests.AddRange(testMethods);
            }
            return tests;
        }

        protected abstract IEnumerable<TestMethod> GenerateTestMethodsFor(ConstructorMetadata metadata);

        protected static bool IsDefaultConstructor(ConstructorInfo constructor)
        {
            return !constructor.GetParameters().Any();
        }

        private static IEnumerable<ConstructorInfo> GetAccessibleContructors(Type t, bool internalsVisible)
        {
            return t.GetConstructors(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.CreateInstance | BindingFlags.Instance)
                .Where(x => !x.IsPrivate 
                         && !x.IsFamily
                         && (!x.IsAssembly || internalsVisible));
        }

        private static IEnumerable<ConstructorInfo> GetCtorsWithExplicitParameterCast(IEnumerable<ConstructorInfo> ctorsToTest)
        {
            return ctorsToTest
                              .GroupBy(x => x.GetParameters().Length)
                              .Where(x => x.Count() > 1)
                              .SelectMany(x => x).ToArray();
        }

        private static bool HasMultipleParametrizedConstructors(IEnumerable<ConstructorInfo> ctorsToTest)
        {
            return ctorsToTest.Where(ctor => ctor.GetParameters().Any()).Count() > 1;
        }

    }
}
