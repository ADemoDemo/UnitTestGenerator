using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestGenerator.UnitTestGeneration
{
    public class TypeContext
    {
        public TypeContext(Type targetType, bool internalsVisible)
        {
            Check.NotNull(targetType, nameof(targetType));
            TargetType = targetType;
            InternalsVisible = internalsVisible;
        }

        public Type TargetType { get; private set; }
        public bool InternalsVisible { get; private set; }
    }
}
