using Document_Lab7.Colleagues;
using Document_Lab7.Mediator;
using Document_Lab7.Models;

namespace Document_Lab7
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Console.WriteLine("🖨️  ЗАПУСК СИСТЕМЫ УПРАВЛЕНИЯ ПЕЧАТЬЮ\n");

            // 1. Создаём коллег
            var queue = new PrintQueue();
            var printer = new Printer();
            var logger = new Logger();
            var dispatcher = new Dispatcher();

            // 2. Создаём посредника и связываем всех коллег
            var mediator = new PrintMediator(queue, printer, logger);
            dispatcher.SetMediator(mediator);

            // 3. Создаём документы
            var doc1 = new Document("Отчет_2024");
            var doc2 = new Document("Договор_ООО");
            var doc3 = new Document("Накладная_№42");

            // Инжектируем посредника в документы (для доступа из состояний)
            doc1.Mediator = mediator;
            doc2.Mediator = mediator;
            doc3.Mediator = mediator;

            // 4. Демонстрация: добавление в очередь
            System.Console.WriteLine("📥 Этап 1: Добавление документов в очередь");
            dispatcher.CommandAddToQueue(doc1);
            dispatcher.CommandAddToQueue(doc2);
            dispatcher.CommandAddToQueue(doc3);
            mediator.Notify(null, "ProcessQueue");
            // 5. Демонстрация: попытка нарушить правила состояний
            System.Console.WriteLine("\n🚫 Этап 2: Попытка некорректных операций");
            doc1.Print(); // OK: New -> RequestPrint
            doc1.AddToQueue(); // Ошибка: уже в процессе

            // 6. Демонстрация: симуляция ошибки принтера
            System.Console.WriteLine("\n⚠️ Этап 3: Имитация сбоя принтера");
            var doc4 = new Document("Спецификация_Х");
            doc4.Mediator = mediator;
            printer.SimulateFailure = true; // Следующая печать упадёт
            dispatcher.CommandAddToQueue(doc4);

            // 7. Демонстрация: восстановление и повтор
            System.Console.WriteLine("\n🔧 Этап 4: Восстановление после ошибки");
            printer.Fix(); // Чиним принтер
            dispatcher.CommandRetry(doc4); // Reset + Enqueue

            // 8. Завершение
            System.Console.WriteLine("\n✅ Демонстрация завершена.");
            System.Console.WriteLine("Нажмите любую клавишу для выхода...");
            System.Console.ReadKey();
        }
    }
}
