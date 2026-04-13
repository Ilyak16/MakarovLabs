using Document_Lab7.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using AppDoc = Document_Lab7.Models.Document;
using System.Threading.Tasks;

namespace Document_Lab7.States
{
    public class ErrorState : IDocumentState
    {
        public void Print(AppDoc document) =>
            System.Console.WriteLine("[FSM: Error] Печать невозможна из-за ошибки. Сначала сбросьте документ (Reset).");

        public void AddToQueue(AppDoc document) =>
            System.Console.WriteLine("[FSM: Error] Нельзя добавить в очередь из-за ошибки. Сначала сбросьте документ.");

        public void CompletePrinting(AppDoc document) =>
            System.Console.WriteLine("[FSM: Error] Ошибка не устранена.");

        public void FailPrinting(AppDoc document) =>
            System.Console.WriteLine("[FSM: Error] Документ уже в состоянии ошибки.");

        public void Reset(AppDoc document)
        {
            document.SetState(new NewState());
            System.Console.WriteLine("[FSM: Error -> New] Документ сброшен и готов к повторной печати.");
        }
    }
}
