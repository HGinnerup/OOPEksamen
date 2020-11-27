using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace OopEksamen.Classes
{
    internal class ActionLogger : IDisposable
    {
        public string LogPath { get; private set; }
        private FileStream _fileStream { get; set; }
        private Encoding _encoding { get; set; }


        public ActionLogger(string logPath)
        {
            LogPath = logPath;
            
            Directory.CreateDirectory(Path.GetDirectoryName(logPath)); // Ensure existance of directory
            _fileStream = File.OpenWrite(LogPath);
            _encoding = Encoding.UTF8;
        }
        public ActionLogger(string logPath, Encoding encoding)
        {
            LogPath = logPath;
            _fileStream = File.OpenWrite(LogPath);
            _encoding = encoding;
        }

        private string GetTimeStamp() => DateTime.Now.ToString("[YYYY-MM-DD hh:mm:ss] ");

        private void Write(string str)
        {
            _fileStream.Write(_encoding.GetBytes(str));
        }

        public void Log(string str)
        {
            Write(GetTimeStamp());
            Write(str);
            Write(Environment.NewLine);
        }

        public void Dispose()
        {
            _fileStream.Dispose();
        }
    }
}
