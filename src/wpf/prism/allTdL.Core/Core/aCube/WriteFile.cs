using System.IO;

namespace allTdL.Core.aCube
{
    public class WriteFile : ReadOnlyFile
    {
        private string _filePath;
        
        public WriteFile(string filePath) : base(filePath)
        {
            _filePath = filePath;
        }

        public void Write(string contents)
        {
            //var fs = File.Create(_filePath);
            File.WriteAllText(_filePath, contents);
        }
    }
}