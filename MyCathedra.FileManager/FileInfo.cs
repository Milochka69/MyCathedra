using System;

namespace MyCathedra.FileManager
{
    public class FileInfo
    {
        public string Name { get; set; }
        public DateTime UpdateUtc { get; set; }
        public string Path { get; set; }
        public bool IsFle { get; set; }
        public string UserName { get; set; }
    }
}