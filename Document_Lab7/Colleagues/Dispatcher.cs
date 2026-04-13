using System;
using AppDoc = Document_Lab7.Models.Document;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Document_Lab7.Colleagues
{
    public class Dispatcher : Colleague
    {
        public void CommandAddToQueue(AppDoc document)
        {
            document.AddToQueue();
        }

        public void CommandProcessQueue()
        {
            Mediator?.Notify(this, "ProcessQueue");
        }

        public void CommandRetry(AppDoc document)
        {
            document.Reset();
            document.AddToQueue();
        }
    }
}
