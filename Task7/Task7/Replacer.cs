using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Task7
{
    public class Replacer
    {
        public readonly Dictionary<string, string> Replacements;

        public Replacer()
        {
            Replacements = new();
        }

        public Replacer(string pathToDictionary)
        {
            Replacements = new();
            ReadDictionaryFromFIle(pathToDictionary);
        }

        public void ReadDictionaryFromFIle(string pathToDictionary)
        {
            if (String.IsNullOrWhiteSpace(pathToDictionary))
            {
                throw new ArgumentException("Argument is either null or whitespace.", nameof(pathToDictionary));
            }
            if (!File.Exists(pathToDictionary))
            {
                throw new FileNotFoundException("File not found.", pathToDictionary);
            }

            if (String.Compare(new FileInfo(pathToDictionary).Extension, ".txt") != 0)
            {
                throw new ArgumentOutOfRangeException(nameof(pathToDictionary), "Only text (.txt) files are supported.");
            }

            using (StreamReader reader = new(pathToDictionary))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                { 
                    MatchCollection replacements = Regex.Matches(line, @"\w+\s\-\s\w+");
                    if (replacements.Count == 0)
                    {
                        continue;
                    }
                    foreach (Match replacement in replacements)
                    {
                        string[] keyValue = replacement.Value.Split(" - ");
                        Replacements.Add(keyValue[0], keyValue[1]);
                    }
                }
            }
        }
        public string Translate(string text)
        {
            if (String.IsNullOrWhiteSpace(text))
            {
                throw new ArgumentException("Argument is either null or whitespace.", nameof(text));
            }

            MatchCollection mWords = Regex.Matches(text, @"\w+");
            List<string> words = mWords.Select(mWord => mWord.Value).ToList();
            foreach (string word in words)
            {
                if (!Replacements.ContainsKey(word))
                {
                    Console.Write($"Enter replacement for {word}: ");
                    string replacement;
                    while ((replacement = Console.ReadLine()).Length == 0) ;
                    Replacements.Add(word, replacement);
                }

                text = text.Replace(word, Replacements[word]);
            }
            return text;
        }

        public string TranslateTextFromFile(string pathToText)
        {
            if (String.IsNullOrWhiteSpace(pathToText))
            {
                throw new ArgumentException("Argument is either null or whitespace.", nameof(pathToText));
            }
            if (!File.Exists(pathToText))
            {
                throw new FileNotFoundException("File not found.", pathToText);
            }

            if (String.Compare(new FileInfo(pathToText).Extension, ".txt") != 0)
            {
                throw new ArgumentOutOfRangeException(nameof(pathToText), "Only text (.txt) files are supported.");
            }

            string text = File.ReadAllText(pathToText);
            return Translate(text);
        }
    }
}
