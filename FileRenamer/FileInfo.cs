using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileRenamer
{
    public class FileInfo
    {
        public string Path { get; set; }
        public string FileName { get;  private set; }
        public string Extension { get; private set; }
        public string NewName { get; set; }
        public bool Selected { get; set; }

        public FileInfo(string path, string fileName, string extension)
        {
            Path = path;
            FileName = fileName;
            Extension = extension;
            NewName = "";
            Selected = false;
        }

    }
}
