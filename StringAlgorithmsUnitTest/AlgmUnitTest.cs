using StringAlgorithms;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace StringAlgorithmsUnitTest
{
    public class AlgmUnitTest
    {
        [Fact]
        public void SearchTest()
        {
            var algms = new List<ISubstringSearch>()
            {
                new BruteForceAlgorithm(),
            };

            string text = "aaaaabaaaaa"; //10
            string pattern = "aa";
            var expected = Enumerable.Range(0, 10).ToList();
            expected.RemoveRange(4, 2);
            foreach (var algm in algms)
            {
                var actual = algm.IndexesOf(pattern, text);
                Assert.Equal(expected, actual);
            }
        }
    }
}