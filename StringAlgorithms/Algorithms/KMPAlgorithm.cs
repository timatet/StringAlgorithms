using System.Collections.Generic;

namespace StringAlgorithms
{
    public class KMPAlgorithm : ISubstringSearch
    {
        public string GetName() => "KMPAlgorithm";
        public IEnumerable<int> IndexesOf(string pattern, string text)
        {
            //throw new NotImplementedException();
            return new List<int>();
        }
    }
}
