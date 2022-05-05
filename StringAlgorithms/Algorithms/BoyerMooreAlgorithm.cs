using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading;

namespace StringAlgorithms
{
    public class BoyerMooreAlgorithm : ISubstringSearch
    {
        //int[] stopCharsOffsetTable;
        Dictionary<char, int> stopCharsOffsetTable;
        int universalOffset;
        public string GetName() => "BoyerMooreAlgorithm";

        /// <summary>
        /// Инициализация таблицы стоп-символов. Для всех символов паттерна это позиция смещения последнего символа от конца строки.
        /// Для всех остальных символов и для последнего символа это длина паттерна.
        /// </summary>
        public void InitStopCharsOffsetTable(string pattern)
        {
            int patternLength = pattern.Length;
            stopCharsOffsetTable = new Dictionary<char, int>();
            universalOffset = patternLength;

            for (int i = patternLength - 2, offset = 1; i >= 0; i--, offset++)
            {
                if (!stopCharsOffsetTable.ContainsKey(pattern[i]))
                    stopCharsOffsetTable.Add(pattern[i], offset);
            }
        }

        private bool CheckStringAtIndex(int index, string pattern, string text)
        {
            int patternLength = pattern.Length;
            for (int i = patternLength; i > 0; i--)
            {
                if (pattern[i - 1] != text[index--])
                    return false;
            }

            return true;
        }

        private int GetOffset(int index, string pattern, string text)
        {
            if (pattern[pattern.Length - 1] != text[index])
            {
                if (!stopCharsOffsetTable.ContainsKey(text[index]))
                    return universalOffset;
                return stopCharsOffsetTable[text[index]];
            } else
            {
                if (!stopCharsOffsetTable.ContainsKey(pattern[pattern.Length - 1]))
                    return universalOffset;
                return stopCharsOffsetTable[pattern[pattern.Length - 1]];
            }
        }

        public IEnumerable<int> IndexesOf(string pattern, string text)
        {
            List<int> matchesIndexes = new List<int>();
            int textLength = text.Length;
            int patternLength = pattern.Length;
            InitStopCharsOffsetTable(pattern);

            int endOfPatternInTextIndex = patternLength - 1;

            while (endOfPatternInTextIndex < textLength)
            {
                bool patternIsMatch = CheckStringAtIndex(endOfPatternInTextIndex, pattern, text);
                if (!patternIsMatch)
                {
                    int offset = GetOffset(endOfPatternInTextIndex, pattern, text);
                    endOfPatternInTextIndex += offset;
                }
                else
                {
                    matchesIndexes.Add(endOfPatternInTextIndex - patternLength + 1);
                    endOfPatternInTextIndex++;
                }
            }

            return matchesIndexes;
        }

        public BoyerMooreAlgorithm()
        {
            stopCharsOffsetTable = new Dictionary<char, int>();
        }
    }
}
