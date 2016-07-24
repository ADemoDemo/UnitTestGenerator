using System;
using System.Linq;

namespace UnitTestGenerator
{
    /// <summary>
    /// Provides extension methods for <see cref="Type"/>.
    /// </summary>
    public static class TypeExtensions
    {
        /// <summary>
        /// Extract safe name of a <see cref="Type"/>.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string GetSafeTypeName(this Type type)
        {
            if (type.IsGenericType)
            {
                return type.Name.Substring(0, type.Name.IndexOf('`')) + string.Join("", type.GetGenericArguments().Select(arg => arg.GetSafeTypeName()));
            }
            if (type.IsArray)
            {
                return type.GetElementType().GetSafeTypeName() + "Array";
            }
            return type.Name;
        }
    }
}
