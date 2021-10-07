using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task5
{
    public class Replacer
    {
        private string[] _text;
        public Replacer(string filePath)
        {
            ReadTextFromFile(filePath);
        }

        public void ReadTextFromFile(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("File not found.", filePath);
            }

            if (String.CompareOrdinal(new FileInfo(filePath).Extension, ".txt") != 0)
            {
                throw new ArgumentOutOfRangeException(nameof(filePath), filePath, "Only text files are supported (*.txt).");
            }
            _text = File.ReadAllLines(filePath);
            if (!CheckForEven())
            {
                throw new ArgumentException("Count of hash symbols in the text must be even.");
            }
        }

        private bool CheckForEven()
        {
            return _text.Sum(str => str.Count(ch => ch == '#')) % 2 == 0;
        }
        public string[] Replace()
        {
            if (!_text.Where(str => str.Count(ch => ch == '#') > 0).Any())
            {
                return _text;
            }

            int pos = 0, strPos, rpos = 0, strrPos;
            for (strPos = 0; strPos < _text.Length; ++strPos)
            {
                if ((pos = _text[strPos].IndexOf('#')) != -1)
                    break;
            }

            for (strrPos = _text.Length - 1; strrPos >= 0; --strrPos)
            {
                if ((rpos = _text[strrPos].LastIndexOf('#')) != -1)
                    break;
            }

            StringBuilder sb = new(_text[strPos]);
            sb[pos] = '<';
            _text[strPos] = sb.ToString();
            sb = new(_text[strrPos]);
            sb[rpos] = '>';
            _text[strrPos] = sb.ToString();
            return Replace();
        }
        public override string ToString()
        {
            return String.Join("\n", Replace());
        }
    }
}
