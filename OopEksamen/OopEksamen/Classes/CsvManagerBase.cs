using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace OopEksamen.Classes
{
    abstract class CsvManagerBase<T> : IDisposable
    {

        public CsvManagerBase(string filePath, char delimiter = ',', string newLine = null, Encoding encoding = null)
        {
            FilePath = FilePath;
            _fileStream = File.Open(filePath, FileMode.Open);
            _delimiter = delimiter;

            if (newLine is null) newLine = Environment.NewLine;
            _newLine = newLine;

            if (encoding is null) encoding = Encoding.UTF8;
            _encoding = encoding;
        }

        private Type[] _types { get; set; }
        private char _delimiter { get; set; }
        private string _newLine { get; set; }
        private Encoding _encoding { get; set; }

        protected abstract T DataParse(string[] data);
        protected abstract string[] DataEncode(T data);


        protected string FilePath { get; private set; }
        protected string Delimeter { get; private set; }

        private FileStream _fileStream { get; set; }


        private string DataToString(T data)
        {
            return String.Join(Delimeter, DataEncode(data).ToArray());
        }
        private byte[] StringToBytes(string str)
        {
            return _encoding.GetBytes(str);
        }
        private byte[] DataToBytes(T data)
        {
            return StringToBytes(DataToString(data));
        }

        protected void AppendData(T data) {
            _fileStream.Write(DataToBytes(data));
            _fileStream.Write(StringToBytes(Environment.NewLine));
        }

        protected IEnumerable<T> GetData()
        {
            using var reader = new StreamReader(_fileStream);
            while(true)
            {
                var line = reader.ReadLine();
                if (line == null) break;
                if (line == string.Empty) break;

                var lineSplit = line.Split(Delimeter);

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
