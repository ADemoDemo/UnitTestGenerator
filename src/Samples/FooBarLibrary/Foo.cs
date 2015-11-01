namespace FooBarLibrary
{
    public class Foo
    {
        public string Concat(string strA, string strB)
        {
            if (strB == null)
                throw new System.ArgumentNullException(nameof(strB));
            if (strA == null)
                throw new System.ArgumentNullException(nameof(strA));
            return strA + strB;
        }

        public string Concat(string strA, string strB, string strC)
        {
            return strA + strB + strC;
        }

        public string Lowercase(string value)
        {
            return value.ToLowerInvariant();
        }
    }
}
