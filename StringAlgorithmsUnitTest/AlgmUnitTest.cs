using StringAlgorithms;
using System.Collections.Generic;
using Xunit;

namespace StringAlgorithmsUnitTest
{
    public class AlgmUnitTest
    {
        [Theory]
        [InlineData("", "", new int[] { 0 })]
        [InlineData("a", "aaa", new int[] { 0, 1, 2 })]
        [InlineData("aa", "aaaaaa", new int[] { 0, 1, 2, 3, 4 })]
        [InlineData("aa", "aabaaa", new int[] { 0, 3, 4 })]
        [InlineData("abc", "abcabcabc", new int[] { 0, 3, 6 })]
        public void SearchTest(string pattern, string text, IEnumerable<int> expectedPositions)
        {
            var algms = new List<ISubstringSearch>()
            {
                new BruteForceAlgorithm(),
            };

            foreach (var algm in algms)
            {
                var actual = algm.IndexesOf(pattern, text);
                Assert.Equal(expectedPositions, actual);
            }
        }
    }
}