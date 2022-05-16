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
            List<int> matchesIndexes = new List<int>();

            var prefixFunction = PrefixFunction(pattern);

            int textLength = text.Length;
            int patternLength = pattern.Length;

            int i = 0, j = 0;

            while (i < textLength)
            {
                if (pattern[j] == text[i])
                {
                    j++;
                    i++;
                }

                if (j == patternLength)
                {
                    matchesIndexes.Add(i - j);
                    j = prefixFunction[j - 1];
                }
                else if (i < textLength && pattern[j] != text[i])
                {
                    if (j != 0)
                    {
                        j = prefixFunction[j - 1];
                    }
                    else
                    {
                        i += 1;
                    }
                }
            }

            return matchesIndexes;
        }
    }
}
