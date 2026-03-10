using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Builder_Lab4_.Class
{
    public sealed class PrototypeRegistry
    {
        private static readonly Lazy<PrototypeRegistry> _instance = new Lazy<PrototypeRegistry>(() => new PrototypeRegistry()); 
        public static PrototypeRegistry Instance => _instance.Value;
        private Dictionary<string, Computer> _prototypes;
        private PrototypeRegistry()
        {
            _prototypes = new Dictionary<string, Computer>();
            Console.WriteLine("Реестр прототипов инициализирован");
        }
        public void Register(string key, Computer prototype)
        {
            _prototypes[key] = prototype.DeepCLone();
            Console.WriteLine($"Прототип {key} Инициализирован");
        }
        public Computer GetPrototype(string key)
        {
            if (_prototypes.ContainsKey(key)) return _prototypes[key].DeepCLone();
            throw new ArgumentException($"Прототип {key} не найден. ");
        }

    }
}
