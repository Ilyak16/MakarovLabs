using Document_Lab7.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using AppDoc = Document_Lab7.Models.Document;
using System.Text;
using System.Threading.Tasks;

namespace Document_Lab7.States
{
    public class PrintingState : IDocumentState
    {
        public void Print(AppDoc document) =>
            System.Console.WriteLine("[FSM: Printing] Документ уже печатается.");

        public void AddToQueue(AppDoc document) =>
            System.Console.WriteLine("[FSM: Printing] Нельзя добавить в очередь: документ в процессе печати.");

        public void CompletePrinting(AppDoc document)
        {
            document.SetState(new DoneState());
            System.Console.WriteLine("[FSM: Printing -> Done] Печать завершена успешно.");
        }

        public void FailPrinting(AppDoc document)
        {
            document.SetState(new ErrorState());
            System.Console.WriteLine("[FSM: Printing -> Error] Произошла ошибка печати.");
        }

        public void Reset(AppDoc document) =>
            System.Console.WriteLine("[FSM: Printing] Нельзя сбросить: документ в процессе печати.");
    }
}
