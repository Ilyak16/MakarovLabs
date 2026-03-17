using Lab_5__GoF__.Class;
using Lab_5__GoF__.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_5__GoF__.Facade
{
    public class FileManagerFacade
    {
        /// <summary>
        /// Внутренний метод: строит дерево Composite на основе данных из Адаптера
        /// </summary>
        private FolderItem BuildCompositeTree(IStorageProvider storage, string path)
        {
            var pathParts = path.Split('/');
            var folderName = pathParts.LastOrDefault() ?? "Root";
            var parentPath = string.Join("/", pathParts.Take(pathParts.Length - 1));

            var root = new FolderItem(folderName, parentPath);

            var contents = storage.ListContents(path);
            foreach (var item in contents)
            {
                if (item.Type == "file")
                {
                    // В реальной системе здесь был бы рекурсивный спуск для подпапок
                    var fileNode = new FileItem(item.Name, item.Size, path);
                    root.Add(fileNode);
                }
            }
            return root;
        }

        /// <summary>
        /// Сценарий: Резервное копирование
        /// </summary>
        public void Backup(IStorageProvider source, IStorageProvider dest,
                          string sourcePath, string destPath)
        {
            Console.WriteLine("\nЗАПУСК РЕЗЕРВНОГО КОПИРОВАНИЯ");
            Console.WriteLine($"   Источник: {source.GetName()}");
            Console.WriteLine($"   Назначение: {dest.GetName()}");

            // 1. Подключение через Адаптеры
            if (!source.Connect() || !dest.Connect())
            {
                Console.WriteLine("Ошибка подключения!");
                return;
            }

            // 2. Построение структуры (Composite) для анализа
            Console.WriteLine("Построение иерархии файлов (Composite)...");
            var tree = BuildCompositeTree(source, sourcePath);

            // 3. Использование Composite для рекурсивного подсчета
            int totalSize = tree.GetSize();
            Console.WriteLine($"   📊 Обнаружено файлов. Общий размер: {totalSize} байт.");
            Console.WriteLine("   📋 Структура:");
            tree.Display(indent: 4);

            // 4. Выполнение копирования (скрытая сложность)
            Console.WriteLine("   🔄 Копирование данных...");
            var files = source.ListContents(sourcePath);
            foreach (var f in files)
            {
                string srcFilePath = $"{sourcePath}/{f.Name}";
                string dstFilePath = $"{destPath}/{f.Name}";

                byte[] data = source.ReadFile(srcFilePath);
                // Имитация задержки сети
                Thread.Sleep(100);
                dest.WriteFile(dstFilePath, data);
                Console.WriteLine($"{f.Name} скопирован");
            }

            Console.WriteLine("Резервное копирование завершено успешно!");
        }

        /// <summary>
        /// Сценарий: Синхронизация (упрощенно - просто анализ)
        /// </summary>
        public void Sync(IStorageProvider storage, string path)
        {
            Console.WriteLine($"\nСИНХРОНИЗАЦИЯ {storage.GetName()}");
            var tree = BuildCompositeTree(storage, path);
            Console.WriteLine($"Размер для синхронизации: {tree.GetSize()} байт");
            tree.Display(indent: 4);
        }
    }
}
