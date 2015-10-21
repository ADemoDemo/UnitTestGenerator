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
namespace UnitTestGenerator.CodeGeneration.Generators
{
    /// <summary>
    /// Defines the methods that are required for generating a test method source code.
    /// </summary>
    /// <typeparam name="TRequest"></typeparam>
    public interface ITestMethodSourceCodeGenerator<TRequest>
    {
        /// <summary>
        /// Builds source code based on request object.
        /// </summary>
        /// <param name="request">The request object which contains information about the target.</param>
        /// <returns>The generated string containing the generated source code.</returns>
        string BuildSourceCode(TRequest request);

        /// <summary>
        /// Builds the method name.
        /// </summary>
        /// <param name="request">The request object which contains information about the target.</param>
        /// <returns>The generated string containing name of method.</returns>
        string BuildMethodName(TRequest request);
    }
}
