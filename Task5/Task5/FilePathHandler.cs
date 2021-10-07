using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task5
{
    public class FilePathHandler
    {
        private string _filePath;
        public FilePathHandler(string filePath)
        {
            _filePath = filePath;
        }

        public string GetFileName()
        {
            int rpos = _filePath.LastIndexOf('\\');
            if (rpos < 0)
            {
                throw new InvalidOperationException("Couldn't get file name.");
            }
            return _filePath.Substring(rpos + 1);
        }

        public string GetRootDirectory()
        {
            int pos = _filePath.IndexOf('\\');
            if (pos < 0)
            {
                throw new InvalidOperationException("Couldn't get root directory.");
            }
            return _filePath.Substring(0, pos + 1);
        }
    }
}
