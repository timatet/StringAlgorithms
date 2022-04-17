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

            Console.Write("Enter path to text: ");
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() != true)
            {
                return;
            }

            string pathToText = openFileDialog.FileName;
            Console.WriteLine(pathToText);
            StreamReader reader = new StreamReader(pathToText);
            string text = reader.ReadToEnd();
            reader.Close();

            string wordForSearching = null;
            Console.WriteLine("Enter the words to search for. To exit, leave the field empty.");
            while (wordForSearching != string.Empty)
            {
                Console.Write("Enter word for searching: ");
                wordForSearching = Console.ReadLine();

                if (string.IsNullOrEmpty(wordForSearching))
                {
                    return;
                }

                SearchString(wordForSearching, text);
            }
        }
        static void InitAlgms()
        {
            foreach (var algm in algorithms)
            {
                algm.IndexesOf("", "");
            }
        }
        static void SearchString(string pattern, string text)
        {
            Console.WriteLine("*****************************************");
            Stopwatch stopwatch = new Stopwatch();
            foreach (var algm in algorithms)
            {
                stopwatch.Restart();
                var indexes = algm.IndexesOf(pattern, text);
                stopwatch.Stop();

                Console.WriteLine("{0}: {1}ticks; Found {2} matches.", nameof(algm), stopwatch.ElapsedTicks, indexes.Count());
            }
            Console.WriteLine();
        }
    }
}
