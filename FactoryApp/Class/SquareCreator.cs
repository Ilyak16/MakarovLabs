using FactoryApp.Interface;

namespace FactoryApp.Class
{
    public class SquareCreator : ShapeCreator
    {
        public override IShape CreateShape()
            => new Square();
    }
}