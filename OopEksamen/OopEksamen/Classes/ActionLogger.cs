using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace OopEksamen.Classes
{
    internal class ActionLogger
    {
        public string LogPath { get; private set; }
        private Encoding _encoding { get; set; }

        public ActionLogger(string logPath, Encoding encoding = null)
        {
            LogPath = logPath;
            Directory.CreateDirectory(Path.GetDirectoryName(logPath)); // Ensure existance of directory

            if (encoding == null) encoding = Encoding.UTF8;
            _encoding = encoding;
        }

        private string GetTimeStamp() => DateTime.Now.ToString("u", DateTimeFormatInfo.InvariantInfo);

        public void Log(string str)
        {
            using var fileStream = File.Open(LogPath, FileMode.Append);

            fileStream.Write(_encoding.GetBytes($"[{GetTimeStamp()}] {str}{Environment.NewLine}"));
        }
    }
}
