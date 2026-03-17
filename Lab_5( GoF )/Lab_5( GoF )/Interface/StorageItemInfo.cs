using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_5__GoF__.Interface
{
    public class StorageItemInfo
    {
        public string Name { get; set; } = "";
        public string Type { get; set; } = ""; // "file" или "folder"
        public int Size { get; set; }
    }
}
