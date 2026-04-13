using Document_Lab7.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using AppDoc = Document_Lab7.Models.Document;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Document_Lab7.States
{
    public class DoneState : IDocumentState
    {
        public void Print(AppDoc document) =>
            System.Console.WriteLine("[FSM: Done] Документ уже напечатан.");

        public void AddToQueue(AppDoc document) =>
            System.Console.WriteLine("[FSM: Done] Нельзя добавить: документ уже завершён.");

        public void CompletePrinting(AppDoc document) =>
            System.Console.WriteLine("[FSM: Done] Документ уже в финальном состоянии.");

        public void FailPrinting(AppDoc document) =>
            System.Console.WriteLine("[FSM: Done] Ошибка невозможна: документ уже напечатан.");

        public void Reset(AppDoc document) =>
            System.Console.WriteLine("[FSM: Done] Финальное состояние. Сброс невозможен.");
    }
}
