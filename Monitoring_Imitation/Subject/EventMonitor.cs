using Monitoring_Imitation.Delegates;
using Monitoring_Imitation.Events;
using Monitoring_Imitation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monitoring_Imitation.Subject
{
    public class EventMonitor
    {
        public event MetricEventHandler? OnMetricExceeded;

        public void CheckMetric(string metricName, double value, double threshold)
        {
            Console.WriteLine($"[Monitor]: Checking {metricName} ({value} vs {threshold})");

            if (value > threshold)
            {
                // ✅ Создаём данные метрики (как требовалось в задании)
                var eventData = new MetricData(metricName, value, threshold, DateTime.Now);

                // Вызываем событие — все подписчики получат уведомление
                OnMetricExceeded?.Invoke(new MetricEventArgs(metricName + "_Exceeded", eventData));
            }
        }

        // Метод для имитации непрерывного мониторинга
        public async Task MonitorAsync(IEnumerable<(string Name, double Value, double Threshold)> metrics,
                                       int intervalMs = 1000)
        {
            foreach (var (name, value, threshold) in metrics)
            {
                CheckMetric(name, value, threshold);
                await Task.Delay(intervalMs);
            }
        }
    }
}
