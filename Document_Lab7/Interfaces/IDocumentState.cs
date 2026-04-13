using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using AppDoc = Document_Lab7.Models.Document;
using System.Threading.Tasks;

namespace Document_Lab7.Interfaces
{
    public interface IDocumentState
    {
        void Print(AppDoc document);
        void AddToQueue(AppDoc document);
        void CompletePrinting(AppDoc document);
        void FailPrinting(AppDoc document);
        void Reset(AppDoc document);
    }
}
