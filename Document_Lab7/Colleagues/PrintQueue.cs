using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppDoc = Document_Lab7.Models.Document;

namespace Document_Lab7.Colleagues
{
    public class PrintQueue : Colleague
    {
        private readonly Queue<AppDoc> _queue = new Queue<AppDoc>();

        public void Enqueue(AppDoc document)
        {
            _queue.Enqueue(document);
            Mediator?.Notify(this, "Enqueued", document);
        }

        public AppDoc Dequeue()
        {
            return _queue.Count > 0 ? _queue.Dequeue() : null;
        }

        public bool IsEmpty => _queue.Count == 0;

        public void NotifyQueueEmpty()
        {
            Mediator?.Notify(this, "QueueEmpty");
        }
    }
}
