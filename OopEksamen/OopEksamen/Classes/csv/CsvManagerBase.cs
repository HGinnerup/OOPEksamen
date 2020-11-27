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
        public CsvManagerBase(string filePath, char delimiter = ',', string newLine = null, Encoding encoding = null, uint headerLineCount = 1)
        {
            FilePath = filePath;
            Directory.CreateDirectory(Path.GetDirectoryName(filePath)); // Ensure existance of directory
            _fileStream = File.Open(filePath, FileMode.OpenOrCreate);
            _delimiter = delimiter;

            if (newLine is null) newLine = Environment.NewLine;
            _newLine = newLine;

            if (encoding is null) encoding = Encoding.UTF8;
            _encoding = encoding;

            _headerLineCount = headerLineCount;
        }

        private char _delimiter { get; set; }
        private string _newLine { get; set; }
        private Encoding _encoding { get; set; }
        private uint _headerLineCount { get; set; }

        protected abstract T DataParse(string[] data);
        protected abstract string[] DataEncode(T data);


        protected string FilePath { get; private set; }
        private FileStream _fileStream { get; set; }


        private string DataToString(T data)
        {
            var strings = DataEncode(data).ToArray();
            for(var i=0; i<strings.Length; i++)
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

        protected void AppendData(T data)
        {
            _fileStream.Seek(0, SeekOrigin.End);
            _fileStream.Write(DataToBytes(data));
            _fileStream.Write(StringToBytes(_newLine));
        }

        protected IEnumerable<T> GetData()
        {
            _fileStream.Seek(0, SeekOrigin.Begin);
            using var reader = new StreamReader(_fileStream);

            // Skip headers
            for (var i = 0; i < _headerLineCount; i++) reader.ReadLine();

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

        public virtual void Dispose()
        {
            _fileStream.Dispose();
        }
    }
}
