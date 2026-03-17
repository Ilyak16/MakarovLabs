using Lab_5__GoF__.Adaptees;
using Lab_5__GoF__.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_5__GoF__.Adapters
{
    public class FtpStorageAdapter : IStorageProvider
    {
        private readonly FtpServerAPI _ftpApi = new();
        private readonly string _host;
        private readonly string _user;
        private readonly string _pass;

        public FtpStorageAdapter(string host, string user, string pass)
        {
            _host = host;
            _user = user;
            _pass = pass;
        }

        public bool Connect()
        {
            Console.WriteLine($"  [Adapter] Подключение к FTP {_host}...");
            try
            {
                _ftpApi.Login(_host, _user, _pass);
                return _ftpApi.IsConnected();
            }
            catch
            {
                return false;
            }
        }

        public List<StorageItemInfo> ListContents(string path)
        {
            // АДАПТАЦИЯ: Парсинг специфичного строкового ответа FTP
            var rawLines = _ftpApi.ListFiles(path);
            var result = new List<StorageItemInfo>();

            foreach (var line in rawLines)
            {
                // Эмуляция парсинга строки FTP: "-rw-r--r-- 1 user user       1234 filename"
                var parts = line.Split(new[] { ' ' }, System.StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length >= 5)
                {
                    result.Add(new StorageItemInfo
                    {
                        Name = parts.Last(),
                        Type = "file",
                        Size = int.Parse(parts[parts.Length - 2])
                    });
                }
            }
            return result;
        }

        public byte[] ReadFile(string path)
        {
            return _ftpApi.RetrieveFile(path);
        }

        public bool WriteFile(string path, byte[] data)
        {
            return _ftpApi.StoreFile(path, data);
        }

        public string GetName()
        {
            return $"FTP Server ({_host})";
        }
    }
}
