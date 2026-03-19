using GraphicSystem.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicSystem.Proxy
{
    public class HighResImage : IImage
    {
        private string _filename;

        public HighResImage(string filename)
        {
            _filename = filename;
            LoadFromDisk(); // Тяжелая операция сразу
        }

        private void LoadFromDisk()
        {
            Console.WriteLine($"[Proxy] ЗАГРУЗКА: Чтение файла {_filename} с диска (тяжелая операция)...");
            System.Threading.Thread.Sleep(500); // Имитация задержки
        }

        public void Draw()
        {
            Console.WriteLine($"[Proxy] ОТРИСОВКА: Вывод изображения {_filename} на экран");
        }
    }

    // Заместитель
    public class ImageProxy : IImage
    {
        private string _filename;
        private HighResImage _realImage;

        public ImageProxy(string filename)
        {
            _filename = filename;
        }

        public void Draw()
        {
            // Ленивая инициализация: создаем тяжелый объект только при первом вызове Draw
            if (_realImage == null)
            {
                _realImage = new HighResImage(_filename);
            }
            _realImage.Draw();
        }
    }
}
