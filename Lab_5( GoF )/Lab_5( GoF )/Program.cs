using Lab_5__GoF__.Adapters;
using Lab_5__GoF__.Interface;
namespace Lab_5__GoF__.Facade;

internal class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine(new string('=', 60));
        Console.WriteLine("   ДЕМО: ФАЙЛОВЫЙ МЕНЕДЖЕР (GoF PATTERNS)");
        Console.WriteLine(new string('=', 60));

        // 1. Инициализация Адаптеров (ТРИ системы: Local, FTP, Cloud)
        IStorageProvider localAdapter = new LocalStorageAdapter();

        // Новый FTP адаптер с учетными данными
        IStorageProvider ftpAdapter = new FtpStorageAdapter("ftp.example.com", "user", "password");

        IStorageProvider cloudAdapter = new CloudStorageAdapter();

        // 2. Инициализация Фасада
        var manager = new FileManagerFacade();

        // --- Сценарий 1: Локальная система ---
        manager.Sync(localAdapter, "/docs");

        // --- Сценарий 2: Бэкап Локал -> Облако ---
        manager.Backup(
            source: localAdapter,
            dest: cloudAdapter,
            sourcePath: "/docs",
            destPath: "/backup/local"
        );

        // --- Сценарий 3: FTP -> Облако (Новый сценарий) ---
        // Демонстрирует, что Фасад работает с FTP так же, как и с другими
        manager.Backup(
            source: ftpAdapter,
            dest: cloudAdapter,
            sourcePath: "/ftp/files",
            destPath: "/backup/ftp"
        );

        // --- Финальная проверка ---
        Console.WriteLine("\n🔍 ПРОВЕРКА ОБЛАЧНОГО ХРАНИЛИЩА:");
        manager.Sync(cloudAdapter, "/backup/local");
        manager.Sync(cloudAdapter, "/backup/ftp");

        Console.WriteLine("\n" + new string('=', 60));
        Console.WriteLine("   РАБОТА ЗАВЕРШЕНА");
        Console.WriteLine(new string('=', 60));

        Console.WriteLine("\nНажмите любую клавишу для выхода...");
        Console.ReadKey();
    }
}
