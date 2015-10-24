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
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestGenerator.DynamicProxy
{
    public class CastleProxyGenerator
    {
        Castle.DynamicProxy.ProxyGenerator generator = new Castle.DynamicProxy.ProxyGenerator();

        public TType CreateClassProxy<TType>()
            where TType : class
        {
            var targetType = typeof(TType);
            return (TType)CreateClassProxy(targetType);
        }

        public object CreateClassProxy(Type targetType)
        {
            Check.NotNull(targetType, nameof(targetType));
            if (targetType.IsValueType)
            {
                throw new ProxyGeneratorException(string.Format("Value type {0} is not supported for proxy generation.", targetType.Name));
            }
            if (targetType == typeof(string))
            {
                throw new ProxyGeneratorException(string.Format("Type String is not supported for proxy generation."));
            }
            if (IsTypeAccessible(targetType))
            {
                throw new TypeAccessiblityException(string.Format("Type {0} is not accessible.", targetType.FullName));
            }
            if (targetType.IsInterface)
            {
                return generator.CreateInterfaceProxyWithoutTarget(targetType);
            }

            var defaultCtor = GetContructorsForType(targetType).FirstOrDefault(x => x.GetParameters().Count() == 0);
            if (IsConstructorAccessible(defaultCtor))
            {
                try
                {
                    return generator.CreateClassProxy(targetType);
                }
                catch (Castle.DynamicProxy.ProxyGenerationException x)
                {
                    throw new TypeAccessiblityException("Castle instance creation error", x);
                }
            }

            return UseAnyAvailableConstructor(targetType);
        }

        private object UseAnyAvailableConstructor(Type targetType)
        {
            ConstructorInfo useableCtor = GetUseableConstructor(targetType);
            var parameters = useableCtor.GetParameters().Select(p => CreateClassProxy(p.ParameterType)).ToArray();
            var instance = generator.CreateClassProxy(targetType, parameters);
            return instance;
        }

        private static ConstructorInfo[] GetContructorsForType(Type targetType)
        {
            return targetType.GetConstructors(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.CreateInstance | BindingFlags.Instance);
        }

        private static ConstructorInfo GetUseableConstructor(Type targetType)
        {
            var ctors = GetContructorsForType(targetType);
            var useableCtor = ctors.Where(x => x.IsPublic).OrderBy(x => x.GetParameters().Count()).FirstOrDefault();
            if (useableCtor == null)
            {
                useableCtor = ctors.Where(x => x.IsFamily || (x.IsAssembly && TypesAssemblyHasInternalsVisible(targetType))).OrderBy(x => x.GetParameters().Count()).FirstOrDefault();
            }
            if (useableCtor == null)
            {
                throw new TypeAccessiblityException(string.Format("Cannot find accessible constructor fot type {0}.", targetType.FullName));
            }

            return useableCtor;
        }

        private static bool IsConstructorAccessible(ConstructorInfo ctor)
        {
            return ctor != null
                            && (ctor.IsPublic
                                    || ctor.IsFamily
                                    || (ctor.IsAssembly && TypesAssemblyHasInternalsVisible(ctor.ReflectedType)));
        }

        private static bool IsTypeAccessible(Type targetType)
        {
            var internalsVisible = TypesAssemblyHasInternalsVisible(targetType);
            return !((!targetType.IsNested && (targetType.IsVisible || internalsVisible))
                            || (targetType.IsNestedPublic || (targetType.IsNestedAssembly && internalsVisible)));
        }

        private static bool TypesAssemblyHasInternalsVisible(Type targetType)
        {
            return targetType.Assembly.GetCustomAttributes<InternalsVisibleToAttribute>().Any(attr => attr.AssemblyName == "DynamicProxyGenAssembly2");
        }

        //private bool CheckIfSystem(Type t, ref object result)
        //{
        //    if (t == typeof(MemberInfo))
        //    {
        //        Func<string, string> method = string.Copy;
        //        result = method.Method;
        //        return true;
        //    }
        //    else if (t == typeof(Type))
        //    {
        //        result = typeof(string);
        //        return true;
        //    }
        //    return false;
        //}
    }
}
