using Monitoring_Imitation.Events;
using Monitoring_Imitation.Interfaces;
using Monitoring_Imitation.Subject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monitoring_Imitation.Observers
{
    public class AlertObserver : IDisposable
    {
        private readonly EventMonitor _monitor;
        private readonly IMessageFormatter _formatter;
        private readonly INotificationChannel _channel;

        public string ObserverName { get; }

        public AlertObserver(EventMonitor monitor, string name,
                             IMessageFormatter formatter, INotificationChannel channel)
        {
            _monitor = monitor ?? throw new ArgumentNullException(nameof(monitor));
            ObserverName = name ?? throw new ArgumentNullException(nameof(name));
            _formatter = formatter ?? throw new ArgumentNullException(nameof(formatter));
            _channel = channel ?? throw new ArgumentNullException(nameof(channel));

            // Подписка на событие (паттерн Observer)
            _monitor.OnMetricExceeded += HandleMetricExceeded;
            Console.WriteLine($"[Observer] {ObserverName} subscribed via {_channel.Name}");
        }

        private void HandleMetricExceeded(MetricEventArgs e)
        {
            Console.WriteLine($"\n[{ObserverName}] Processing event: {e.EventType}");
            var formattedMessage = _formatter.Format(e); // Стратегия форматирования
            _channel.Send(formattedMessage);             // Стратегия доставки
        }

        public void Dispose()
        {
            _monitor.OnMetricExceeded -= HandleMetricExceeded;
            Console.WriteLine($"[Observer] {ObserverName} unsubscribed");
        }
    }
}
