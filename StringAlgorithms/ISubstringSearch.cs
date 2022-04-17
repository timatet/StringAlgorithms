using System.Collections.Generic;

namespace StringAlgorithms
{
    public interface ISubstringSearch
    {
        /// <summary>
        /// Возвращает список позиций, начиная с которых строка pattern входит в текст text.
        /// </summary>
        IEnumerable<int> IndexesOf(string pattern, string text);
        string GetName();
    }
}
