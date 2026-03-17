using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_5__GoF__.Class
{
    public class FolderItem : FileSystemItem
    {
        private readonly List<FileSystemItem> _children = new();
        private readonly string _parentPath;

        public FolderItem(string name, string parentPath = "")
            : base(name)
        {
            _parentPath = parentPath;
        }

        public void Add(FileSystemItem item)
        {
            _children.Add(item);
        }

        public void Remove(FileSystemItem item)
        {
            _children.Remove(item);
        }

        public override int GetSize()
        {
            // Рекурсивный подсчет размера всей папки
            return _children.Sum(child => child.GetSize());
        }

        public override void Display(int indent = 0)
        {
            Console.WriteLine(new string(' ', indent) + $"📁 {Name}");
            foreach (var child in _children)
            {
                child.Display(indent + 2);
            }
        }

        public override string GetPath()
        {
            return $"{_parentPath}/{Name}";
        }

        public List<FileSystemItem> GetChildren()
        {
            return new List<FileSystemItem>(_children);
        }
    }
}
