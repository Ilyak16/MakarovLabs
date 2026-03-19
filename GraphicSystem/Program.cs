using GraphicSystem.Bridge;
using GraphicSystem.Decorator;
using GraphicSystem.Flyweight;
using GraphicSystem.Interfaces;
using GraphicSystem.Proxy;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("=== ЗАПУСК СИСТЕМЫ РЕНДЕРИНГА ===\n");

        // 1. ДЕМОНСТРАЦИЯ PATTERNS: BRIDGE
        Console.WriteLine("--- 1. Паттерн Bridge (Разделение фигуры и рендерера) ---");
        IRenderer vector = new VectorRenderer();
        IRenderer raster = new RasterRenderer();

        Shape circle = new Circle(vector, 5.0f);
        circle.Draw(); // Круг через Вектор

        // Меняем реализацию на лету
        circle.SetRenderer(raster);
        circle.Draw(); // Круг через Растр
        Console.WriteLine();

        // 2. ДЕМОНСТРАЦИЯ PATTERNS: PROXY
        Console.WriteLine("--- 2. Паттерн Proxy (Отложенная загрузка) ---");
        // Создаем прокси, но файл еще не загружен
        IImage image = new ImageProxy("huge_photo.jpg");
        Console.WriteLine("Прокси создан. Файл еще не загружен.");

        // Загрузка происходит только здесь
        image.Draw();
        Console.WriteLine("Повторный вызов (без повторной загрузки):");
        image.Draw();
        Console.WriteLine();

        // 3. ДЕМОНСТРАЦИЯ PATTERNS: FLYWEIGHT
        Console.WriteLine("--- 3. Паттерн Flyweight (Оптимизация текста) ---");
        CharacterFactory factory = new CharacterFactory();

        // Создаем "документ" из 5 символов 'A', но объект будет создан только 1 раз
        ICharacter c1 = factory.GetCharacter('A', "Arial", "Red");
        c1.Display(10, 10);

        ICharacter c2 = factory.GetCharacter('A', "Arial", "Red");
        c2.Display(20, 10);

        ICharacter c3 = factory.GetCharacter('B', "Arial", "Red"); // Новый символ, новый объект
        c3.Display(30, 10);

        Console.WriteLine($"Всего создано объектов символов: {factory.GetTotalObjectsCreated()}");
        Console.WriteLine();

        // 4. ДЕМОНСТРАЦИЯ PATTERNS: DECORATOR
        Console.WriteLine("--- 4. Паттерн Decorator (Динамические эффекты) ---");
        Shape baseShape = new Square(vector, 10.0f);

        // Оборачиваем в декораторы
        Shape decorated = new ShadowDecorator(
                            new BorderDecorator(baseShape, "Black")
                          );

        decorated.Draw();

        Console.WriteLine("\n=== РАБОТА ЗАВЕРШЕНА ===");
        Console.ReadKey();
    }
}