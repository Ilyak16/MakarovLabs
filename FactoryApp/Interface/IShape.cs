using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Shapes;

namespace FactoryApp.Interface
{
    public interface IShape
    {
        Shape Draw(double size, Brush color);
    }
}
