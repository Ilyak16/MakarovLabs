using Builder_Lab4_.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Builder_Lab4_.Class
{
    public class ConcreteComputerBuilder : IComputerBuilder
    {
        private Computer _computer;
        public ConcreteComputerBuilder() 
        { 
            _computer = new Computer();
        }
        public void SetCPU(string cpu) => _computer.Cpu = cpu;
        public void SetGPU(string gpu) => _computer.Gpu = gpu;
        public void SetRAM(int ram) => _computer.Ram = ram;
        public void AddExtra (string extra) => _computer.Extras.Add(extra);
        public void Reset()
        {
            _computer = new Computer();
        }
        public Computer GetProduct()
        {
            Computer result = _computer;
            Reset();
            return result;
        }

    }
}
