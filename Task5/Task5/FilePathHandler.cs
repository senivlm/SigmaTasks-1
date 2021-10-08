using System;

namespace Task5
{
    public class FilePathHandler
    {
        public readonly string FilePath;
        public FilePathHandler(string filePath)
        {
            if (String.IsNullOrWhiteSpace(filePath))
            {
                throw new ArgumentException("String is null or whitespace only, which are not allowed.", nameof(filePath));
            }
            FilePath = filePath;
        }

        public string GetFileName()
        {
            int rpos = FilePath.LastIndexOf('\\');
            if (rpos < 0)
            {
                throw new InvalidOperationException("Couldn't get file name.");
            }
            return FilePath.Substring(rpos + 1, FilePath.Length - FilePath.LastIndexOf('.'));
        }

        public string GetRootDirectory()
        {
            int pos = FilePath.IndexOf('\\');
            if (pos < 0)
            {
                throw new InvalidOperationException("Couldn't get root directory.");
            }
            return FilePath.Substring(0, pos + 1);
        }
    }
}
