using Microsoft.Win32;
using StringAlgorithms;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace SubstringSearchingApp
{
    internal class Program
    {
        static List<ISubstringSearch> algorithms = new List<ISubstringSearch>
        {
            new BruteForceAlgorithm(),
            new BoyerMooreAlgorithm(),
            new KMPAlgorithm(),
            new RabinKarpAlgorithm(),
        };
        [STAThread]
        static void Main(string[] args)
        {
            InitAlgms();
            string pathToText;
            if (args.Length > 0)
            {
                pathToText = args[0];
            }
            else
            {
                pathToText = SelectFile("text");
            }

            StreamReader reader = new StreamReader(pathToText);
            string text = reader.ReadToEnd();
            reader.Close();

            Console.WriteLine("To select other text leave the field empty and press Enter. \n" +
                    "To exit, leave the field of file selection empty.");

            string wordForSearching = null;
            while (wordForSearching != string.Empty)
            {
                Console.Write("Enter word for searching: ");
                wordForSearching = Console.ReadLine();

                if (string.IsNullOrEmpty(wordForSearching))
                {
                    Console.Write("You want to select file with words for searching?\n" +
                        "('y' as yes, 'q' to quit OR any string to continue) : ");
                    string inp = Console.ReadLine();
                    if (inp == "y")
                    {
                        string pathToWords = SelectFile("words for searching");
                        var words = GetWords(pathToWords);
                        foreach (var word in words)
                        {
                            SearchString(word, text);
                        }

                        wordForSearching = " ";
                        continue;
                    } else if (inp == "q")
                    {
                        return;
                    }

                    pathToText = SelectFile("text");

                    if (string.IsNullOrEmpty(pathToText))
                    {
                        return;
                    }

                    reader = new StreamReader(pathToText);
                    text = reader.ReadToEnd();
                    reader.Close();

                    wordForSearching = " ";
                    continue;
                }

                SearchString(wordForSearching, text);
            }
        }
        static string[] GetWords(string path)
        {
            StreamReader reader = new StreamReader(path);
            string[] words = reader.ReadToEnd().Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries).ToArray();
            reader.Close();
            return words;
        }
        static string SelectFile(string loc)
        {
            Console.Write("Enter path to {0}: ", loc);
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() != true)
            {
                return string.Empty;
            }

            Console.WriteLine(openFileDialog.FileName);
            return openFileDialog.FileName;
        }
        static void InitAlgms()
        {
            foreach (var algm in algorithms)
            {
                algm.IndexesOf("INIT", "INIT");
            }
        }
        static void SearchString(string pattern, string text)
        {
            Console.WriteLine("**********************************************************");
            Console.WriteLine("Looking for the word \"{0}\".", pattern);
            Stopwatch stopwatch = new Stopwatch();
            foreach (var algm in algorithms)
            {
                stopwatch.Restart();
                var indexes = algm.IndexesOf(pattern, text);
                stopwatch.Stop();

                var algmName = algm.GetName();
                var whiteSpaces = GetWhiteSpaces(25 - algmName.Length);
                Console.WriteLine("{0}:{1}{2}ms;\tFound {3} matches.", algmName, whiteSpaces, stopwatch.ElapsedMilliseconds, indexes.Count());
            }
            Console.WriteLine();
        }
        static string GetWhiteSpaces(int countWhiteSpaces)
        {
            string whiteSpaces = string.Empty;
            while (countWhiteSpaces-- > 0)
            {
                whiteSpaces += " ";
            }
            return whiteSpaces;
        }
    }
}
