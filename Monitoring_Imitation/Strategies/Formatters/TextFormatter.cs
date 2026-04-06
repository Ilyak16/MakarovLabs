using Monitoring_Imitation.Events;
using Monitoring_Imitation.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monitoring_Imitation.Strategies.Formatters
{
    public class TextFormatter : IMessageFormatter
    {
        public string Format(MetricEventArgs eventArgs)
        {
            var data = eventArgs.Data;
            return $"[{eventArgs.EventType}] CRITICAL ALERT!\n" +
                   $"Metric: {data.MetricName}\n" +
                   $"Current Value: {data.Value}\n" +
                   $"Threshold: {data.Threshold}\n" +
                   $"Time: {data.Timestamp:yyyy-MM-dd HH:mm:ss}\n" +
                   new string('-', 40);
        }
    }
}
