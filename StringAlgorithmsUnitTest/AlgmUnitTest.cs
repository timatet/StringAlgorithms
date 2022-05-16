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
        [InlineData("дата", "метадата", new int[] { 4 })]
        [InlineData("данные", "данные", new int[] { 0 })]
        [InlineData("данные", "денные", new int[] { })]
        public void SearchTest(string pattern, string text, IEnumerable<int> expectedPositions)
        {
            var algms = new List<ISubstringSearch>()
            {
                new BruteForceAlgorithm(),
                new BoyerMooreAlgorithm(),
                new RabinKarpAlgorithm()
            };

            foreach (var algm in algms)
            {
                var actual = algm.IndexesOf(pattern, text);
                Assert.Equal(expectedPositions, actual);
            }
        }

        [Fact]
        public void TestTest()
        {
            var bm = new BoyerMooreAlgorithm();
            bm.InitStopCharsOffsetTable("teammast");


        }
    }
}