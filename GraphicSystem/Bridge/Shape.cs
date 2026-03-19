using GraphicSystem.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicSystem.Bridge
{
    public abstract class Shape
    {
        // Инкапсулируем поле, предоставляя доступ наследникам через свойство
        protected IRenderer _renderer;

        protected Shape(IRenderer renderer)
        {
            _renderer = renderer;
        }

        // Свойство для доступа к рендереру из наследников (например, Декоратора)
        internal IRenderer Renderer
        {
            get { return _renderer; }
            set { _renderer = value; }
        }

        public abstract void Draw();

        public void SetRenderer(IRenderer renderer)
        {
            _renderer = renderer;
        }
    }

    public class Circle : Shape
    {
        private float _radius;
        public Circle(IRenderer renderer, float radius) : base(renderer)
        {
            _radius = radius;
        }

        public override void Draw()
        {
            _renderer.RenderCircle(_radius);
        }
    }

    public class Square : Shape
    {
        private float _side;
        public Square(IRenderer renderer, float side) : base(renderer)
        {
            _side = side;
        }

        public override void Draw()
        {
            _renderer.RenderSquare(_side);
        }
    }
}
