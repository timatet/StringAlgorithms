namespace StringAlgorithms
{
    public interface ISubstringSearch
    {
        /// <summary>
        /// Возвращает список позиций, начиная с которых строка pattern входит в текст text.
        /// </summary>
        string IndexesOf(string pattern, string text);
    }
}
