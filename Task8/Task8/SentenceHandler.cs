using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Task8
{
    public class SentenceHandler
    {
        public List<string> Sentences { get; }
        public SentenceHandler(string pathToFileWithText)
        {
            Sentences = new();
            ReadTextFromFile(pathToFileWithText);
        }

        public void ReadTextFromFile(string pathToFileWithText)
        {
            if (!File.Exists(pathToFileWithText))
            {
                throw new FileNotFoundException("File not found.", pathToFileWithText);
            }

            if (String.Compare(new FileInfo(pathToFileWithText).Extension, ".txt") != 0)
            {
                throw new ArgumentOutOfRangeException($"{nameof(pathToFileWithText)}", pathToFileWithText, "Only text files are supported (*.txt).");
            }

            string[] lines = File.ReadAllLines(pathToFileWithText);
            string sentence = "";

            foreach (string line in lines)
            {
                string[] splits = line.Split('.');
                for (int i = 0; i < splits.Length - 1; ++i)
                {
                    sentence += splits[i];
                    Sentences.Add(sentence);
                    sentence = "";
                }
     Пояснити цей рядок
                sentence = splits[^1];
            } 
            Sentences.Add(sentence);
            SortSentencesByLength();
        }

        public void SortSentencesByLength()
        {
            Sentences.Sort((str1, str2) => str1.Length - str2.Length);
        }
        public string GetSentenceWithMaxNestedDepth()
        {
            if (!Sentences.Any())
            {
                throw new InvalidOperationException(
                    "Cannot find sentence with maximum nested parentheses depth due to emptiness of Sentences List."
                    );
            }
            int maxDepth = 0, maxDepthIdx = 0;
            for (int i = 1; i < Sentences.Count; ++i)
            {
                Stack<char> parentheses = new();
                int depth = 0, maxCurrentDepth = 0;
                foreach (char symbol in Sentences[i])
                {
                    if (symbol == '(')
                    {
                        parentheses.Push('(');
                        ++depth;
                    }
                    else if (symbol == ')')
                    {
                        char parenthesis;
                        if ((parenthesis = parentheses.Pop()) == '(')
                        {
                            if (maxCurrentDepth < depth)
                            {
                                maxCurrentDepth = depth;
                            }
                            --depth;
                            if (depth < 0) // Current sentence has invalid order of parentheses.
                            {
                                break;
                            }
                        }
                    }
                }

                if (maxCurrentDepth > maxDepth)
                {
                    maxDepth = maxCurrentDepth;
                    maxDepthIdx = i;
                }
            }

            return Sentences[maxDepthIdx];
        }
    }
}
