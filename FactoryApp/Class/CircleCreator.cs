using FactoryApp.Interface;

namespace FactoryApp.Class
{
    public class CircleCreator : ShapeCreator
    {
        public override IShape CreateShape()
            => new Circle();
    }
}