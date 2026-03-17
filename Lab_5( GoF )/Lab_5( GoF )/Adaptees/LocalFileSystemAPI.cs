using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_5__GoF__.Adaptees
{
    public class LocalFileSystemAPI
    {
        private readonly Dictionary<string, byte[]> _storage = new()
        {
            { "/docs/report.txt", Encoding.UTF8.GetBytes("Report Content") },
            { "/docs/notes.txt", Encoding.UTF8.GetBytes("Notes Content") },
            { "/images/logo.png", new byte[] { 0x89, 0x50, 0x4E, 0x47 } }
        };

        public List<Dictionary<string, object>> ScanDir(string path)
        {
            var results = new List<Dictionary<string, object>>();
            foreach (var kvp in _storage)
            {
                if (kvp.Key.StartsWith(path) && kvp.Key != path)
                {
                    var rel = kvp.Key.Substring(path.Length).TrimStart('/');
                    if (!rel.Contains("/"))
                    {
                        results.Add(new Dictionary<string, object>
                        {
                            { "name", rel },
                            { "type", "file" },
                            { "size", kvp.Value.Length }
                        });
                    }
                }
            }
            return results;
        }

        public byte[] Fetch(string path)
        {
            return _storage.TryGetValue(path, out var data) ? data : Array.Empty<byte>();
        }

        public bool Store(string path, byte[] data)
        {
            _storage[path] = data;
            return true;
        }
    }
}
