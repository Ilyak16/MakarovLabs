using System;
using AppDoc = Document_Lab7.Models.Document;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Document_Lab7.Colleagues
{
    public class Printer : Colleague
    {
        public bool SimulateFailure { get; set; } = false;
        public bool IsBroken { get; private set; } = false;

        public void StartPrint(AppDoc document)
        {
            if (IsBroken)
            {
                System.Console.WriteLine($"[Printer] ❌ Принтер сломан. Печать '{document.Title}' невозможна.");
                Mediator?.Notify(this, "PrintFailed", document);
                return;
            }

            System.Console.WriteLine($"[Printer] 🖨️ Печать '{document.Title}'...");

            if (SimulateFailure)
            {
                SimulateFailure = false;
                IsBroken = true;
                Mediator?.Notify(this, "PrintFailed", document);
            }
            else
            {
                Mediator?.Notify(this, "PrintSuccess", document);
            }
        }

        public void Fix()
        {
            IsBroken = false;
            System.Console.WriteLine("[Printer] 🔧 Принтер починен.");
        }
    }
}
