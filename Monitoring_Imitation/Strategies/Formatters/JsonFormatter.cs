using Monitoring_Imitation.Events;
using Monitoring_Imitation.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Monitoring_Imitation.Strategies.Formatters
{
    public class JsonFormatter : IMessageFormatter
    {
        public string Format(MetricEventArgs eventArgs)
        {
            var payload = new
            {
                eventType = eventArgs.EventType,
                metric = eventArgs.Data.MetricName,
                value = eventArgs.Data.Value,
                threshold = eventArgs.Data.Threshold,
                timestamp = eventArgs.Data.Timestamp.ToString("o")
            };
            return JsonSerializer.Serialize(payload, new JsonSerializerOptions { WriteIndented = true });
        }
    }
}
