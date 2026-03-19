using GraphicSystem.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicSystem.Bridge
{
    public class VectorRenderer : IRenderer
    {
        public void RenderCircle(float radius) => Console.WriteLine($"[Vector] Рисуем круг радиусом {radius}");
        public void RenderSquare(float side) => Console.WriteLine($"[Vector] Рисуем квадрат со стороной {side}");
        public void RenderText(string content) => Console.WriteLine($"[Vector] Рисуем текст: {content}");
        public string GetName() => "Векторный рендерер";
    }

    public class RasterRenderer : IRenderer
    {
        public void RenderCircle(float radius) => Console.WriteLine($"[Raster] Пикселизация круга радиусом {radius}");
        public void RenderSquare(float side) => Console.WriteLine($"[Raster] Пикселизация квадрата со стороной {side}");
        public void RenderText(string content) => Console.WriteLine($"[Raster] Растровый текст: {content}");
        public string GetName() => "Растровый рендерер";
    }
}
