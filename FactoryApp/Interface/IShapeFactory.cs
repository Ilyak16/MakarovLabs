using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryApp.Interface
{
    public interface IShapeFactory
    {
        IShape CreateCircle();
        IShape CreateSquare();
        IShape CreateTriangle();
    }
}
