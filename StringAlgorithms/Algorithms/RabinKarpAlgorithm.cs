using System;
using System.Collections.Generic;

namespace StringAlgorithms
{
    //public class RabinKarpAlgorithm : ISubstringSearch
    //{

    //    public string GetName() => "RabinKarpAlgorithm";
    //    public IEnumerable<int> IndexesOf(string pattern, string text)
    //    {
    //        List<int> offsets = new List<int>();
    //        byte step = 127;
    //        byte module = 17;
    //        int patternHash = 0;
    //        int textHash = 0;
    //        int offset = 1;
    //        if (pattern.Length > text.Length) { return offsets; }
    //        for (int i = 0; i < pattern.Length; ++i)
    //        {
    //            patternHash = (patternHash << step) + pattern[i];
    //            patternHash -= patternHash >> module << module;
    //            textHash = (textHash << step) + text[i];
    //            textHash -= (textHash >> module << module);
    //        }

    //        if (patternHash == textHash)
    //        {
    //            bool flag = true;
    //            for (int l = 0; l < pattern.Length; l++) { if (text[l] != pattern[l]) { flag = false; break; } }
    //            if (flag) offsets.Add(0);
    //        }
    //        int pow = 1;
    //        for (var i = 1; i < pattern.Length; i++)
    //        {
    //            if (pow == 0) break;
    //            pow <<= step;
    //            pow -= (pow >> module << module);
    //        }
    //        for (int i = pattern.Length; i < text.Length; i++)
    //        {
    //            textHash = ((textHash - pow * text[i - 1]) << step) + text[i];
    //            textHash -= textHash >> module << module;
    //            if (textHash == patternHash)
    //            {
    //                bool flag = true;
    //                for (int l = 0; l < pattern.Length; l++)
    //                { if (text[offset + l] != pattern[l]) { flag = false; break; } }
    //                if (flag) offsets.Add(offset);
    //            }
    //            offset++;
    //        }
    //        return offsets;
    //    }
    //}
    public class RabinKarpAlgorithm : ISubstringSearch
    {
        public int Radix;
        public int Mod;
        public RabinKarpAlgorithm(int radix, int mod)
        {
            Radix = radix;
            Mod = mod;
        }
        public string GetName() => "RabinKarpAlgorithm";
        public IEnumerable<int> IndexesOf(string pattern, string text)
        {
            List<int> res = new List<int>();
            int textLength = text.Length;
            int patternLength = pattern.Length;
            if (textLength == 0 || patternLength == 0) return res;
            int h = (int)Math.Pow(Radix, patternLength - 1) % Mod;
            int p = 0;
            int ts = 0;
            for (int i = 0; i < patternLength; i++)
            {
                p = (p * Radix + pattern[i]) % Mod;
                ts = (ts * Radix + text[i]) % Mod;
            }
            p = (p + Mod) % Mod;
            ts = (ts + Mod) % Mod;

            for (int i = 0; i <= textLength - patternLength; i++)
            {
                if (p == ts)
                {
                    bool flag = true;
                    for (int j = i; j < i + patternLength; j++)
                    {
                        if (pattern[j - i] != text[j])
                        {
                            flag = false;
                            break;
                        }
                    }
                    if (flag) res.Add(i);
                }
                else
                {
                    ts = (Radix * (ts - text[i] * h) + text[i + patternLength]) % Mod;
                    ts = (ts + Mod) % Mod;
                }
            }

            return res;
        }
    }
}
