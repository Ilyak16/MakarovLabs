using GraphicSystem.Bridge;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicSystem.Decorator
{
    public abstract class ShapeDecorator : Shape
    {
        protected Shape _decoratedShape;

        public ShapeDecorator(Shape shape) : base(shape.Renderer) // Исправлено: доступ через свойство
        {
            _decoratedShape = shape;
        }

        public override void Draw()
        {
            _decoratedShape.Draw();
        }
    }

    // Конкретный декоратор: Рамка
    public class BorderDecorator : ShapeDecorator
    {
        private string _borderColor;

        public BorderDecorator(Shape shape, string color) : base(shape)
        {
            _borderColor = color;
        }

        public override void Draw()
        {
            _decoratedShape.Draw();
            AddBorder();
        }

        private void AddBorder()
        {
            Console.WriteLine($"[Decorator] Добавлена рамка цвета {_borderColor}");
        }
    }

    // Конкретный декоратор: Тень
    public class ShadowDecorator : ShapeDecorator
    {
        public ShadowDecorator(Shape shape) : base(shape) { }

        public override void Draw()
        {
            _decoratedShape.Draw();
            AddShadow();
        }

        private void AddShadow()
        {
            Console.WriteLine($"[Decorator] Добавлена тень");
        }
    }
}
