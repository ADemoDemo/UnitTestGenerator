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

namespace UnitTestGenerator
{
    /// <summary>
    /// Provides methods for argument validation.
    /// </summary>
    [DebuggerStepThrough]
    public static class Check
    {
        public static T NotNull<T>([ValidatedNotNull] T value, string parameterName) where T : class
        {
            if (parameterName == null)
            {
                throw new ArgumentNullException(nameof(parameterName));
            }

            if (value == null)
            {
                throw new ArgumentNullException(parameterName);
            }

            return value;
        }

        public static T? NotNull<T>([ValidatedNotNull] T? value, string parameterName) where T : struct
        {
            if (parameterName == null)
            {
                throw new ArgumentNullException(nameof(parameterName));
            }

            if (value == null)
            {
                throw new ArgumentNullException(parameterName);
            }

            return value;
        }

        public static string NotEmpty([ValidatedNotNull] string value, string parameterName)
        {
            NotNull(value, parameterName);
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException(Strings.ArgumentIsNullOrWhitespace(parameterName), parameterName);
            }

            return value;
        }

        public static IEnumerable<T> NotEmpty<T>([ValidatedNotNull] IEnumerable<T> enumerable, string parameterName)
        {
            NotNull(enumerable, parameterName);
            if (!enumerable.Any())
            {
                throw new ArgumentException(Strings.ArgumentIsNullOrEmpty(parameterName), parameterName);
            }
            return enumerable;
        }
    }
}
