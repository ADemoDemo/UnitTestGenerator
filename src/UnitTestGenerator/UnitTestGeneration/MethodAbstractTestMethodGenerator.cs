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

namespace UnitTestGenerator.UnitTestGeneration
{
    /// <summary>
    /// Provides a base class for generation of collection of <see cref="TestMethod"/> based on passed <see cref="Type"/> for methods. 
    /// </summary>
    public abstract class MethodAbstractTestMethodGenerator : ITestMethodGenerator
    {
        protected MethodAbstractTestMethodGenerator()
        {
        }

        /// <summary>
        /// Generates a enumeration of <see cref="TestMethod"/>.
        /// </summary>
        /// <param name="typeContext">The context for which the methods are generated.</param>
        /// <returns>The enumeration of <see cref="TestMethod"/>.</returns>
        public IEnumerable<TestMethod> GenerateTestMethods(TypeContext typeContext)
        {
            var tests = new List<TestMethod>();
            var methodsToTest = GetMethodsToTest(typeContext.TargetType, typeContext.InternalsVisible);
            var explicitMethodParameters = GetMethodsWithExplicitParameterCast(methodsToTest);

            foreach (var method in methodsToTest)
            {
                var metadata = new MethodMetadata(method, explicitMethodParameters.Contains(method));
                var testMethods = GenerateTestMethodsFor(metadata);
                tests.AddRange(testMethods);
            }
            return tests;
        }

        private static IEnumerable<MethodInfo> GetMethodsWithExplicitParameterCast(IEnumerable<MethodInfo> methodsToTest)
        {
            return methodsToTest
                            .GroupBy(x => x.Name + x.GetParameters().Length)
                            .Where(x => x.Count() > 1)
                            .SelectMany(x => x).ToArray();
        }

        private static IEnumerable<MethodInfo> GetMethodsToTest(Type type, bool internalsVisible)
        {
            return type.GetMethods(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance)
                            .Where(x => x.IsPublic || x.IsAssembly && internalsVisible)
                            .Where(x => !x.IsSpecialName
                                        && x.DeclaringType == x.ReflectedType
                                        && x.GetParameters().Count() > 0
                                        && x.GetParameters().All(par => !par.ParameterType.IsByRef))
                            .ToArray();
        }
        
        private static bool AssignableParameters(ParameterInfo[] leftMethodParameters, ParameterInfo[] rightMethodParameters)
        {
            for (int i = 0; i < leftMethodParameters.Length; i++)
            {
                if (!leftMethodParameters[i].ParameterType.IsAssignableFrom(rightMethodParameters[i].ParameterType))
                {
                    return false;
                }
            }
            return true;
        }

        protected abstract IEnumerable<TestMethod> GenerateTestMethodsFor(MethodMetadata metadata);
    }
}
