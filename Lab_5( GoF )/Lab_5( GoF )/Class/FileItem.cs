using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_5__GoF__.Class
{
    public class FileItem : FileSystemItem
    {
        private readonly int _size;
        private readonly string _parentPath;

        public FileItem(string name, int size, string parentPath = "")
            : base(name)
        {
            _size = size;
            _parentPath = parentPath;
        }

        public override int GetSize()
        {
            return _size;
        }

        public override void Display(int indent = 0)
        {
            Console.WriteLine(new string(' ', indent) + $"📄 {Name} ({_size} байт)");
        }

        public override string GetPath()
        {
            return $"{_parentPath}/{Name}";
        }
    }
}
