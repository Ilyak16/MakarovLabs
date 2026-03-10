using Builder_Lab4_.Class;

namespace Builder_Lab4_
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Конфигуратор Компьютерных Систем (GoF Patterns) ===\n");
            Console.WriteLine("--- 1. Использование Builder и Factory ---");
            var builder = new ConcreteComputerBuilder();
            var factory = new ComputerFactory(builder);

            var officePC = factory.CreateOfficePC();
            var gamingPC = factory.CreateGamingPC();

            Console.WriteLine($"Офисный ПК: {officePC}");
            Console.WriteLine($"Игровой ПК: {gamingPC}");
            Console.WriteLine();
            Console.WriteLine("--- 2. Использование Singleton (Реестр) ---");
            var registry = PrototypeRegistry.Instance;

            registry.Register("StandardGaming", gamingPC);

            var registry2 = PrototypeRegistry.Instance;
            Console.WriteLine($"Один ли это объект реестра? {ReferenceEquals(registry, registry2)}");
            Console.WriteLine();
            Console.WriteLine("--- 3. Использование Prototype (Клонирование) ---");

            var clonedPC = registry.GetPrototype("StandardGaming");
            Console.WriteLine($"Оригинал из реестра: {gamingPC}");
            Console.WriteLine($"Клон из реестра:    {clonedPC}");
            Console.WriteLine("\n--- Тест целостности данных (Deep Copy) ---");

            clonedPC.Extras.Add("Hacker Sticker");
            clonedPC.Ram = 64;

            Console.WriteLine($"Клон после изменений: {clonedPC}");
            Console.WriteLine($"Оригинал (должен остаться без изменений): {gamingPC}");

            if (gamingPC.Extras.Contains("Hacker Sticker"))
            {
                Console.WriteLine("ОШИБКА: Произошло поверхностное копирование!");
            }
            else
            {
                Console.WriteLine("УСПЕХ: Глубокое копирование сработало корректно.");
            }

            Console.WriteLine("\nНажмите любую клавишу для выхода...");
            Console.ReadKey();
        }
    }
}
