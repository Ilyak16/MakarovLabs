using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicSystem.Interfaces
{
    public interface IRenderer
    {
        void RenderCircle(float radius);
        void RenderSquare(float side);
        void RenderText(string content);
        string GetName();
    }
}
