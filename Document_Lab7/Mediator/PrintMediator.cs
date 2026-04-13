using Document_Lab7.Colleagues;
using Document_Lab7.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppDoc = Document_Lab7.Models.Document;

namespace Document_Lab7.Mediator
{
    public class PrintMediator : IMediator
    {
        private readonly PrintQueue _queue;
        private readonly Printer _printer;
        private readonly Logger _logger;
        private bool _isProcessing = false;

        public PrintMediator(PrintQueue queue, Printer printer, Logger logger)
        {
            _queue = queue;
            _printer = printer;
            _logger = logger;

            _queue.SetMediator(this);
            _printer.SetMediator(this);
            _logger.SetMediator(this);
        }

        public void Notify(Colleague sender, string ev, AppDoc document = null)
        {
            switch (ev)
            {
                case "Enqueued":
                    _logger.Log($"Документ '{document.Title}' добавлен в очередь.");
                    if (!_isProcessing) ProcessQueue();
                    break;

                case "RequestPrint":
                    _logger.Log($"Запрос на печать: '{document.Title}'.");
                    _printer.StartPrint(document);
                    break;

                case "PrintSuccess":
                    document.CompletePrinting();
                    _logger.Log($"✅ Документ '{document.Title}' успешно напечатан.");
                    ProcessQueue();
                    break;

                case "PrintFailed":
                    document.FailPrinting();
                    _logger.Log($"❌ Ошибка печати документа '{document.Title}'.");
                    _isProcessing = false;
                    break;

                case "ProcessQueue":
                    ProcessQueue();
                    break;

                case "QueueEmpty":
                    _logger.Log("📭 Очередь пуста.");
                    _isProcessing = false;
                    break;
            }
        }

        private void ProcessQueue()
        {
            if (_queue.IsEmpty)
            {
                _queue.NotifyQueueEmpty();
                return;
            }

            _isProcessing = true;
            var doc = _queue.Dequeue();
            doc.Print();
        }
    }
}
