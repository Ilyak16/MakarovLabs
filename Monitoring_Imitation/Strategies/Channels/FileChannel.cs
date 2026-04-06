using Monitoring_Imitation.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monitoring_Imitation.Strategies.Channels
{
    public class FileChannel : INotificationChannel
    {
        private readonly string _filePath;
        public string Name => $"File:{_filePath}";

        public FileChannel(string filePath)
        {
            _filePath = filePath ?? throw new ArgumentNullException(nameof(filePath));
        }

        public void Send(string message)
        {
            try
            {
                File.AppendAllText(_filePath, $"[{DateTime.Now:O}] {message}\n\n");
                Console.WriteLine($"[FileChannel] Saved to {_filePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[FileChannel] Error: {ex.Message}");
            }
        }
    }
}
