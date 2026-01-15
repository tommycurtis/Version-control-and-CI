namespace CSCI106
{
    public class FileWriter : IDisposable
    {
        private StreamWriter File;

        public static FileWriter FromAbsolutePath(string path) => new()
        {
            File = new StreamWriter(path),
        };

        public void WriteLine(string text) => File.WriteLine(text);

        public void Write(string text) => File.Write(text);

        public void Dispose() => File.Dispose();
    }
}
