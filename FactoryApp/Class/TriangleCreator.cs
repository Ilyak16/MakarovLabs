using FactoryApp.Interface;

namespace FactoryApp.Class
{
    public class TriangleCreator : ShapeCreator
    {
        public override IShape CreateShape()
            => new Triangle();
    }
}