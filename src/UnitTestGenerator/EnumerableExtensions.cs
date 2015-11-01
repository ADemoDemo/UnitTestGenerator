using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestGenerator
{
    public static class EnumerableExtensions
    {
        public static bool AllUnique<TSource>(this IEnumerable<TSource> source)
        {
            return source.Count() == source.Distinct().Count();
        }
    }
}
