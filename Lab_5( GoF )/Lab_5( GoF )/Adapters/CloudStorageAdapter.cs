using Lab_5__GoF__.Adaptees;
using Lab_5__GoF__.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_5__GoF__.Adapters
{
    public class CloudStorageAdapter : IStorageProvider
    {
        private readonly CloudStorageAPI _api = new();
        private readonly string _prefix = "backup/";

        public bool Connect()
        {
            Console.WriteLine("  [Adapter] Авторизация в Облаке...");
            return true;
        }

        public List<StorageItemInfo> ListContents(string path)
        {
            // Адаптация странного метода ListBlobs
            var raw = _api.ListBlobs(_prefix);
            return raw.Select(i => new StorageItemInfo
            {
                Name = i["fileName"].ToString() ?? "",
                Type = "file",
                Size = (int)i["byteCount"]
            }).ToList();
        }

        public byte[] ReadFile(string path)
        {
            return _api.DownloadBlob(path);
        }

        public bool WriteFile(string path, byte[] data)
        {
            return _api.UploadBlob(path, data);
        }

        public string GetName()
        {
            return "Google Cloud Storage";
        }
    }
}
