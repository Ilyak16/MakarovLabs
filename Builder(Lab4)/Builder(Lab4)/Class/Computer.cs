using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Builder_Lab4_.Class
{
    public class Computer : ICloneable
    {
        public string Gpu {  get; set; }
        public string Cpu { get; set; }
        public int Ram  { get; set; }
        public List<string> Extras { get; set; }
        public Computer() 
        {
            Extras = new List<string>();
        }
        public override string ToString()
        {
            return $"[CPU: {Cpu}, GPU: {Gpu}, RAM: {Ram}, Extras: {string.Join(",", Extras)}";
        }
        public object Clone()
        {
            return this.MemberwiseClone();
        }
        public Computer DeepCLone()
        {
            Computer copy = new Computer 
            { 
                Cpu = this.Cpu,
                Ram = this.Ram,
                Gpu = this.Gpu,
                Extras = new List<string>(this.Extras)
            };
            return copy;
        }
    }
}
