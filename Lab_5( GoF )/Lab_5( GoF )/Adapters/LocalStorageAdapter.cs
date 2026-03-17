using Lab_5__GoF__.Adaptees;
using Lab_5__GoF__.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_5__GoF__.Adapters
{
    public class LocalStorageAdapter : IStorageProvider
    {
        private readonly LocalFileSystemAPI _api = new();
        private readonly string _root = "/docs";

        public bool Connect()
        {
            Console.WriteLine("  [Adapter] Подключение к локальной ФС...");
            return true;
        }

        public List<StorageItemInfo> ListContents(string path)
        {
            var raw = _api.ScanDir(path);
            // Нормализация данных под интерфейс
            return raw.Select(i => new StorageItemInfo
            {
                Name = i["name"].ToString() ?? "",
                Type = "file",
                Size = (int)i["size"]
            }).ToList();
        }

        public byte[] ReadFile(string path)
        {
            return _api.Fetch(path);
        }

        public bool WriteFile(string path, byte[] data)
        {
            return _api.Store(path, data);
        }

        public string GetName()
        {
            return "Local Disk (C:)";
        }
    }
}
