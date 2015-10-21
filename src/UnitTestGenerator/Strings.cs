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
using System.Globalization;

namespace UnitTestGenerator
{
    /// <summary>
    /// Provides exception messages.
    /// </summary>
    internal static class Strings
    {
        /// <summary>
        /// Provides string for NullArgumentException.
        /// </summary>
        /// <param name="parameterName">Name of parameter</param>
        /// <returns></returns>
        internal static string ArgumentIsNullOrWhitespace(string parameterName)
        {
            return string.Format(CultureInfo.InvariantCulture, "The argument '{0}' cannot be null, empty or contain only white space.", parameterName);
        }

        /// <summary>
        /// Provides string for NullArgumentException.
        /// </summary>
        /// <param name="parameterName">Name of parameter</param>
        /// <returns></returns>
        internal static string ArgumentIsNullOrEmpty(string parameterName)
        {
            return string.Format(CultureInfo.InvariantCulture, "The argument '{0}' cannot be null or empty.", parameterName);
        }


        /// <summary>
        /// Provides string for ArgumentException when argument type is not with expected base type.
        /// </summary>
        /// <param name="parameterName">Name of parameter</param>
        /// <returns></returns>
        internal static string ArgumentIsNotBaseClass(Type passedType)
        {
            return string.Format(CultureInfo.InvariantCulture, "Passed type {0} is not a base class or is from a different assembly", passedType.FullName);
        }
    }
}
