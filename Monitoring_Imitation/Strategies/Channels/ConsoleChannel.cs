using Monitoring_Imitation.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monitoring_Imitation.Strategies.Channels
{
    public class ConsoleChannel : INotificationChannel
    {
        public string Name => "Console";

        public void Send(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\n>>> [CHANNEL: {Name}] <<<");
            Console.ResetColor();
            Console.WriteLine(message);
        }
    }
}
