using FactoryApp.Interface;
using System.Windows.Media;
using System.Windows.Shapes;

namespace FactoryApp.Class
{
    public abstract class ShapeCreator
    {
        public abstract IShape CreateShape();
        public Shape DrawShape(double size, Brush color)
        {
            var shape = CreateShape();
            return shape.Draw(size, color);
        }
    }
}