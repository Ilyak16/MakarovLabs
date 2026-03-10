using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Builder_Lab4_.Class
{
    public class ComputerFactory
    {
        private readonly ConcreteComputerBuilder _builder;
        public ComputerFactory(ConcreteComputerBuilder builder)
        {
            _builder = builder;
        }
        public Computer CreateOfficePC()
        {
            _builder.SetCPU("Intel Core i3");
            _builder.SetGPU("Integrated Graphics");
            _builder.SetRAM(8);
            _builder.AddExtra("Office License");
            return _builder.GetProduct();
        }
        public Computer CreateGamingPC()
        {
            _builder.SetCPU("Intel Core i9");
            _builder.SetGPU("NVIDEA RTX 4090");
            _builder.SetRAM(32);
            _builder.AddExtra("RGB Lighting");
            _builder.AddExtra("Liquid Cooling");
            return _builder.GetProduct();
        }
        public Computer CreateHomePC()
        {
            _builder.SetCPU("Intel Core i5");
            _builder.SetGPU("NVIDEA GTX 1660");
            _builder.SetRAM(8);
            _builder.AddExtra("Wi-Fi Module");
            return _builder.GetProduct();
        }
    }
}
