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
using System.Diagnostics;
using System.Reflection;

namespace UnitTestGenerator
{
    /// <summary>
    /// Represent an unit test method.
    /// </summary>
    [DebuggerDisplay("TestMethod: {Name}")]
    public class TestMethod
    {
        private readonly string name;
        private readonly Type shouldThrowException;
        private readonly string sourceCode;
        private readonly MemberInfo testedMember;

        /// <summary>
        /// Initializes a new instance of the TestMethod class.
        /// </summary>
        /// <param name="testedMember">Member of tested Class, eg: Method, Constructor, Property</param>
        /// <param name="name">The final name of the test method.</param>
        /// <param name="sourceCode">Generated source code of test method.</param>
        /// <param name="shouldThrowException"><see cref="Type" /> of exception to be thrown by test case. Must be an Exception. Optional.</param>
        public TestMethod(MemberInfo testedMember, 
            string name,
            string sourceCode,
            Type shouldThrowException = null)
        {
            Check.NotNull(testedMember, nameof(testedMember));
            Check.NotEmpty(name, nameof(name));
            Check.NotEmpty(sourceCode, nameof(sourceCode));
            if (!IsExceptionType(shouldThrowException))
            {
                throw new ArgumentException("Argument shouldThrowException must be of Exception type.", nameof(shouldThrowException));
            }

            this.testedMember = testedMember;
            this.name = name;
            this.sourceCode = sourceCode;
            this.shouldThrowException = shouldThrowException;
        }

        private static bool IsExceptionType(Type shouldThrowException)
        {
            return shouldThrowException == null || typeof(Exception).IsAssignableFrom(shouldThrowException);
        }


        /// <summary>
        /// Gets the name of test.
        /// </summary>
        public string Name
        {
            get
            {
                return name;
            }
        }

        /// <summary>
        /// Gets the exception type which is expected to be thrown in unit test.
        /// </summary>
        public Type ShouldThrowException
        {
            get
            {
                return shouldThrowException;
            }
        }

        /// <summary>
        /// Gets the source code of the unit test method.
        /// </summary>
        public string SourceCode
        {
            get
            {
                return sourceCode;
            }
        }

        /// <summary>
        /// Gets the member of class which the unit test is covering.
        /// </summary>
        public MemberInfo TestedMember
        {
            get
            {
                return testedMember;
            }
        }
    }
}
