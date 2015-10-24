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
using System.Text;
using System.Threading.Tasks;

namespace UnitTestGenerator.DynamicProxy
{
    /// <summary>
    /// Static helper methods which use <see cref="CastleProxyGenerator"/>.
    /// </summary>
    public static class ProxyGenerator
    {
        static CastleProxyGenerator generator = new CastleProxyGenerator();

        /// <summary>
        /// Creates an instance of proxy class.
        /// </summary>
        /// <param name="targetType">The type from which the proxy class is inherited.</param>
        /// <returns>The proxy class instance.</returns>
        public static object CreateProxy(Type type)
        {
            return generator.CreateClassProxy(type);
        }

        /// <summary>
        /// Creates an instance of proxy class.
        /// </summary>
        /// <typeparam name="TType">The type from which the proxy class is inherited.</typeparam>
        /// <returns>The proxy class instance.</returns>
        public static TType CreateProxy<TType>()
            where TType : class
        {
            return generator.CreateClassProxy<TType>();
        }
    }
}
