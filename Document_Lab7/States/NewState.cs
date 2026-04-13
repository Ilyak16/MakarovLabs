using Document_Lab7.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using AppDoc = Document_Lab7.Models.Document;
using System.Text;
using System.Threading.Tasks;

namespace Document_Lab7.States
{
    public class NewState : IDocumentState
    {
        public void Print(AppDoc document)
        {
            document.Mediator?.Notify(document, "RequestPrint", document);
        }

        public void AddToQueue(AppDoc document)
        {
            document.Mediator?.Notify(document, "Enqueued", document);
        }

        public void CompletePrinting(AppDoc document) =>
            System.Console.WriteLine("[FSM: New] Документ ещё не печатался.");

        public void FailPrinting(AppDoc document) =>
            System.Console.WriteLine("[FSM: New] Ошибка невозможна: печать не начата.");

        public void Reset(AppDoc document) =>
            System.Console.WriteLine("[FSM: New] Документ уже в начальном состоянии.");
    }
}

