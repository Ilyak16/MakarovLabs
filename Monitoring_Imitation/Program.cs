using Monitoring_Imitation.Factories;
using Monitoring_Imitation.Observers;
using Monitoring_Imitation.Subject;

namespace Monitoring_Imitation
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("🚀 Запуск системы мониторинга...\n");
            Console.WriteLine(new string('=', 50));

            // Создаём издателя (Subject)
            var monitor = new EventMonitor();

            // Создаём наблюдателей с разными комбинациями стратегий
            using var consoleTextObserver = new AlertObserver(
                monitor,
                "Console-Text",
                FormatterFactory.Create(FormatterFactory.FormatType.Text),
                ChannelFactory.Create(ChannelFactory.ChannelType.Console)
            );

            using var consoleJsonObserver = new AlertObserver(
                monitor,
                "Console-JSON",
                FormatterFactory.Create(FormatterFactory.FormatType.Json),
                ChannelFactory.Create(ChannelFactory.ChannelType.Console)
            );

            using var fileHtmlObserver = new AlertObserver(
                monitor,
                "File-HTML",
                FormatterFactory.Create(FormatterFactory.FormatType.Html),
                ChannelFactory.Create(ChannelFactory.ChannelType.File, "alerts.html")
            );

            // Имитация потока метрик
            var metrics = new List<(string, double, double)>
        {
            ("CPU_Usage", 45.0, 80.0),      // OK
            ("Memory_Usage", 92.5, 85.0),   // ⚠️ Превышение!
            ("Network_In", 300.0, 500.0),   // OK
            ("CPU_Usage", 95.0, 80.0),      // ⚠️ Превышение!
            ("Disk_Usage", 78.0, 90.0),     // OK
            ("Memory_Usage", 88.1, 85.0),   // ⚠️ Превышение!
        };

            Console.WriteLine("\n📡 Начинаем мониторинг метрик...\n");
            await monitor.MonitorAsync(metrics, intervalMs: 500);

            Console.WriteLine("\n" + new string('=', 50));
            Console.WriteLine("✅ Мониторинг завершён. Нажмите Enter для выхода...");
            Console.ReadLine();
        }
    }
}
