using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_5__GoF__.Interface
{
    public interface IStorageProvider
    {
        bool Connect();
        List<StorageItemInfo> ListContents(string path);
        byte[] ReadFile(string path);
        bool WriteFile(string path, byte[] data);
        string GetName();
    }
}
