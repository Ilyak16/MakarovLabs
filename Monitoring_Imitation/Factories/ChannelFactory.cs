using Monitoring_Imitation.Interfaces;
using Monitoring_Imitation.Strategies.Channels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monitoring_Imitation.Factories
{
    public static class ChannelFactory
    {
        public enum ChannelType { Console, File }

        public static INotificationChannel Create(ChannelType type, string? filePath = null) => type switch
        {
            ChannelType.Console => new ConsoleChannel(),
            ChannelType.File => new FileChannel(filePath ?? "alerts.log"),
            _ => throw new ArgumentException($"Unknown channel: {type}")
        };

        public static INotificationChannel Create(string type, string? filePath = null) =>
            Enum.TryParse<ChannelType>(type, true, out var result)
                ? Create(result, filePath)
                : throw new ArgumentException($"Unknown channel: {type}");
    }
}
