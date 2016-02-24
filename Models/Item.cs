using System;
using System.Diagnostics;
using System.IO;

namespace WindowsService1
{
    public class Item : IItem
    {
        public string GetTime()
        {
            return DateTime.Now.ToString();
        }

        public string GetPath()
        {
            Process process = Process.GetCurrentProcess();
            FileInfo info = new FileInfo(process.MainModule.FileName);
            return info.FullName;
        }
    }
}