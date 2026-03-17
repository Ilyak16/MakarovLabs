using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_5__GoF__.Class
{
    public abstract class FileSystemItem
    {
        public string Name { get; protected set; }

        protected FileSystemItem(string name)
        {
            Name = name;
        }

        public abstract int GetSize();
        public abstract void Display(int indent = 0);
        public abstract string GetPath();
    }
}
