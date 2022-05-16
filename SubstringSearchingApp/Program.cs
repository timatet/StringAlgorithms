using Microsoft.Win32;
using StringAlgorithms;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;

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
                Console.Write("Enter path to text: ");
                OpenFileDialog openFileDialog = new OpenFileDialog();
                if (openFileDialog.ShowDialog() != true)
                {
                    return;
                }
                pathToText = openFileDialog.FileName;
            }

            Console.WriteLine(pathToText);
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
                    return; //возвращаемся в меню выбора файла
                }

                SearchString(wordForSearching, text);
            }
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
