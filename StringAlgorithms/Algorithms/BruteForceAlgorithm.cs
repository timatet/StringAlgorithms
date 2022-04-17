using System.Collections.Generic;

namespace StringAlgorithms
{
    public class BruteForceAlgorithm : ISubstringSearch
    {
        public IEnumerable<int> IndexesOf(string pattern, string text)
        {
            int m = pattern.Length;
            int n = text.Length;

            List<int> shiftCollection = new List<int>();
            for (int shift = 0; shift <= n - m; shift++)
            {
                bool SubStringIsEqual = true;
                for (int i = 0; i < m && SubStringIsEqual; i++)
                {
                    if (pattern[i] != text[shift + i])
                    {
                        SubStringIsEqual = false;
                    }
                }
                if (SubStringIsEqual)
                {
                    shiftCollection.Add(shift);
                }
            }

            return shiftCollection;
        }
    }
}
