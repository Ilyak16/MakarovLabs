using Builder_Lab4_.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Builder_Lab4_.Interface
{
    public interface IComputerBuilder
    {
        void Reset();
        void SetCPU(string cpu);
        void SetGPU(string gpu);
        void SetRAM(int ram);
        void AddExtra (string extra);
        Computer GetProduct();
    }
}
