using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_5__GoF__.Adaptees
{
    public class FtpServerAPI
    {
        private readonly Dictionary<string, byte[]> _ftpStorage = new();
        private bool _isLoggedIn = false;

        public void Login(string host, string user, string pass)
        {
            // Эмуляция проверки credentials
            if (!string.IsNullOrEmpty(user) && !string.IsNullOrEmpty(pass))
            {
                _isLoggedIn = true;
                // Добавим тестовые данные при логине
                if (!_ftpStorage.ContainsKey("/ftp/files/data.zip"))
                {
                    _ftpStorage["/ftp/files/data.zip"] = new byte[] { 0x50, 0x4B, 0x03, 0x04 };
                    _ftpStorage["/ftp/files/config.xml"] = System.Text.Encoding.UTF8.GetBytes("<config/>");
                }
            }
        }

        public bool IsConnected()
        {
            return _isLoggedIn;
        }

        public void Disconnect()
        {
            _isLoggedIn = false;
        }

        /// <summary>
        /// Специфичный метод получения списка (возвращает строки формата FTP)
        /// </summary>
        public List<string> ListFiles(string remotePath)
        {
            if (!_isLoggedIn) throw new System.Exception("Not logged in");

            var result = new List<string>();
            foreach (var key in _ftpStorage.Keys)
            {
                if (key.StartsWith(remotePath))
                {
                    var fileName = key.Substring(remotePath.Length).TrimStart('/');
                    if (!fileName.Contains("/")) // Только файлы в текущей директории
                    {
                        var size = _ftpStorage[key].Length;
                        // Формат эмуляции ответа FTP сервера
                        result.Add($"-rw-r--r-- 1 user user {size,10} {fileName}");
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// Скачивание файла в виде потока (эмуляция)
        /// </summary>
        public byte[] RetrieveFile(string remotePath)
        {
            if (!_isLoggedIn) throw new System.Exception("Not logged in");
            return _ftpStorage.TryGetValue(remotePath, out var data) ? data : Array.Empty<byte>();
        }

        /// <summary>
        /// Загрузка файла (STOR)
        /// </summary>
        public bool StoreFile(string remotePath, byte[] data)
        {
            if (!_isLoggedIn) throw new System.Exception("Not logged in");
            _ftpStorage[remotePath] = data;
            return true;
        }
    }
}
