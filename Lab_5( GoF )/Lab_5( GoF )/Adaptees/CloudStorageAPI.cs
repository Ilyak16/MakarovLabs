using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_5__GoF__.Adaptees
{
    public class CloudStorageAPI
    {
        private readonly Dictionary<string, byte[]> _bucket = new();

        public List<Dictionary<string, object>> ListBlobs(string prefix)
        {
            var blobs = new List<Dictionary<string, object>>();
            foreach (var kvp in _bucket)
            {
                if (kvp.Key.StartsWith(prefix))
                {
                    var fileName = kvp.Key.Split('/').Last();
                    blobs.Add(new Dictionary<string, object>
                    {
                        { "fileName", fileName },
                        { "byteCount", kvp.Value.Length }
                    });
                }
            }
            return blobs;
        }

        public byte[] DownloadBlob(string name)
        {
            return _bucket.TryGetValue(name, out var data) ? data : Array.Empty<byte>();
        }

        public bool UploadBlob(string name, byte[] content)
        {
            _bucket[name] = content;
            return true;
        }
    }
}
