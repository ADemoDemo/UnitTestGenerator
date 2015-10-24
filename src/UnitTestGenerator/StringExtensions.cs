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

namespace UnitTestGenerator
{
    /// <summary>
    /// Provides extension methods for <see cref="String"/> type.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Converts first char of string to lowercase.
        /// </summary>
        /// <param name="value">The string to format.</param>
        /// <returns>The formatted string.</returns>
        public static string ToCamelCase(this string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return value;
            }
            if (value.Length == 1)
            {
                return value.ToLowerInvariant();
            }
            return value.Substring(0, 1).ToLowerInvariant() + value.Substring(1);
        }

        /// <summary>
        /// Converts first char of string to uppercase.
        /// </summary>
        /// <param name="value">The string to format.</param>
        /// <returns>The formatted string.</returns>
        public static string Capitalize(this string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return value;
            }
            if (value.Length == 1)
            {
                return value.ToUpperInvariant();
            }
            return value.Substring(0, 1).ToUpperInvariant() + value.Substring(1);
        }

        /// <summary>
        /// Removes the first and last char from string.
        /// </summary>
        /// <param name="value">The string to format.</param>
        /// <returns>The formatted string.</returns>
        public static string RemoveFirstAndLastChar(this string value)
        {
            Check.NotNull(value, nameof(value));
            Debug.Assert(value.Length >= 2);

            if (value.Length < 2)
            {
                throw new ArgumentException("String must be at least 2 char long.", nameof(value));
            }
            return value.Substring(1, value.Length - 2);
        }

        /// <summary>
        /// Removes first and last parentheses in string if are present.
        /// </summary>
        /// <param name="value">The string to format.</param>
        /// <returns>The formatted string.</returns>
        public static string RemoveParantheses(this string value)
        {
            return value.StartsWith("(", StringComparison.Ordinal) && value.EndsWith(")", StringComparison.Ordinal) ? value.RemoveFirstAndLastChar() : value;
        }

    }
}
