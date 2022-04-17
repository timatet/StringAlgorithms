using System.Collections.Generic;

namespace StringAlgorithms
{
    public class BoyerMooreAlgorithm : ISubstringSearch
    {
        public string GetName() => "BoyerMooreAlgorithm";
        public IEnumerable<int> IndexesOf(string pattern, string text)
        {
            //throw new NotImplementedException();
            return new List<int>();
        }
    }
}
