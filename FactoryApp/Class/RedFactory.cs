using FactoryApp.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryApp.Class
{
    public class RedFactory : IShapeFactory
    {
        public IShape CreateCircle() => new Circle();
        public IShape CreateSquare() => new Square();
        public IShape CreateTriangle() => new Triangle();
    }
}
