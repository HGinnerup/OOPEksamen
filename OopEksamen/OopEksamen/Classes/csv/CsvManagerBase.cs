using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace OopEksamen.Classes.Csv
{
    public abstract class CsvManagerBase<T> : IDisposable
    {

        private char _delimiter { get; set; }
        private string _newLine { get; set; }
        private Encoding _encoding { get; set; }
        private IEnumerable<string> _headerLines { get; set; }



        protected abstract T DataParse(string[] data);
        protected abstract string[] DataEncode(T data);


        protected string FilePath { get; private set; }
        private FileStream _fileStream { get; set; }
        public CsvManagerBase(string filePath, IEnumerable<string> headerLines, char delimiter = ',', string newLine = null, Encoding encoding = null)
        {
            FilePath = filePath;
            _headerLines = headerLines;
            _delimiter = delimiter;

            if (newLine is null) newLine = Environment.NewLine;
            _newLine = newLine;

            if (encoding is null) encoding = Encoding.UTF8;
            _encoding = encoding;

            _fileStream = OpenStream();
        }

        protected IEnumerable<T> GetData()
        {
            if (!_fileStream.CanRead) OpenStream();
            _fileStream.Seek(0, SeekOrigin.Begin);
            var reader = new StreamReader(_fileStream);

            // Skip headers
            for (var i = 0; i < _headerLines.Count(); i++) reader.ReadLine();

            // Yield content
            while (true)
            {
                var line = reader.ReadLine();
                if (line == null) break;
                if (line == string.Empty) break;

                var lineSplit = Utilities.StringHandling.SplitString(line, _delimiter).ToArray();

                yield return DataParse(lineSplit);
            }
            yield break;
        }

        private FileStream OpenStream()
        {
            return OpenStream(FilePath);
        }
        private FileStream OpenStream(string filePath)
        {
            bool newFile = !File.Exists(filePath);
            if (newFile)
            {
                Directory.CreateDirectory(Path.GetDirectoryName(filePath));
            }

            var stream = File.Open(filePath, FileMode.OpenOrCreate);

            if (newFile)
            {
                foreach (var header in _headerLines)
                {
                    AppendString(header, stream);
                }
            }

            return stream;
        }

        protected void AppendData(T data)
        {
            AppendString(DataToString(data));
        }
        private void AppendData(T data, FileStream fileStream)
        {
            AppendString(DataToString(data), fileStream);
        }
        private void AppendString(string str, FileStream fileStream)
        {
            fileStream.Seek(0, SeekOrigin.End);
            fileStream.Write(StringToBytes(str));
            fileStream.Write(StringToBytes(_newLine));
        }
        private void AppendString(string str)
        {
            AppendString(str, _fileStream);
        }
        private string DataToString(T data)
        {
            var strings = DataEncode(data).ToArray();
            for (var i = 0; i < strings.Length; i++)
            {
                // Escape quotes
                strings[i] = strings[i].Replace("\"", "\\\"");

                // If contains delimiter, encapsulate in quotes to keep as óne field
                if (strings[i].Contains(_delimiter))
                    strings[i] = $"\"{strings[i]}\"";
            }

            return String.Join(_delimiter, strings);
        }
        private byte[] StringToBytes(string str)
        {
            return _encoding.GetBytes(str);
        }
        private byte[] DataToBytes(T data)
        {
            return StringToBytes(DataToString(data));
        }


        protected void ReWriteFileWithData(IEnumerable<T> data)
        {
            var tmpPath = FilePath + ".tmp";

            var newStream = OpenStream(tmpPath);
            foreach(var row in data)
            {
                AppendData(row, newStream);
            }

            newStream.Dispose();
            _fileStream.Dispose();

            File.Delete(FilePath);
            File.Move(tmpPath, FilePath);

            _fileStream = OpenStream();
        }

        public virtual void Dispose()
        {
            _fileStream.Dispose();
        }
    }
}
