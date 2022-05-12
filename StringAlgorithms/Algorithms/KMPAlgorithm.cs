using System.Collections.Generic;

namespace StringAlgorithms
{
    public class KMPAlgorithm : ISubstringSearch
    {
        public string GetName() => "KMPAlgorithm";
        private int[] PrefixFunction(string pattern)
        {
            int patternLength = pattern.Length;
            var prefixFunction = new int[patternLength];

            for (int i = 1; i < patternLength; ++i)
            {
                int j = prefixFunction[i - 1];
                while (j > 0 && pattern[i] != pattern[j])
                {
                    j = prefixFunction[j - 1];
                }

                if (pattern[i] == pattern[j])
                {
                    ++j;
                }
                prefixFunction[i] = j;
            }

            return prefixFunction;
        }
        public IEnumerable<int> IndexesOf(string pattern, string text)
        {
            //throw new NotImplementedException();
            return new List<int>();
        }
    }
}
