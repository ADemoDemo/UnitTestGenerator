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

using System.Text;

namespace UnitTestGenerator.CodeGeneration.Generators
{
    /// <summary>
    /// Provides a base class which creates the basic structure of a test method source code generation.
    /// </summary>
    /// <typeparam name="TRequest">The concrete request type used to build the source code.</typeparam>
    public abstract class AbstractTestMethodSourceCodeGenerator<TRequest>
    {
        private StringBuilder sourceCode;

        /// <summary>
        /// Builds source code based on request object.
        /// </summary>
        /// <param name="request">The request object which contains information about the target.</param>
        /// <returns>The generated string containing the generated source code.</returns>
        public virtual string BuildSourceCode(TRequest request)
        {
            sourceCode = new StringBuilder();
            BuildArrangeSourceCode(request);
            BuildActSourceCode(request);
            BuildAssertSourceCode(request);

            return sourceCode.ToString();
        }

        /// <summary>
        /// Builds the method name.
        /// </summary>
        /// <param name="request">The object containing information about invoked constructor.</param>
        /// <returns>The generated string containing name of method.</returns>
        public abstract string BuildMethodName(TRequest request);

        protected virtual void BuildAssertSourceCode(TRequest request)
        {
        }

        protected abstract void BuildActSourceCode(TRequest request);

        protected virtual void BuildArrangeSourceCode(TRequest request)
        {
        }

        protected void Append(string value)
        {
            sourceCode.Append(value);
        }

        protected void AppendLine(string value)
        {
            sourceCode.AppendLine(value);
        }

        protected void AppendFormat(string format, object arg)
        {
            sourceCode.AppendFormat(format, arg);
        }
    }
}
