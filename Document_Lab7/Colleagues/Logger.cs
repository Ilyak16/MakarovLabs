using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Document_Lab7.Colleagues
{
    public class Logger : Colleague
    {
        public void Log(string message)
        {
            System.Console.WriteLine($"[📋 LOG] {message}");
        }
    }
}
