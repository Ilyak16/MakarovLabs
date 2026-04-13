using Document_Lab7.Interfaces;
using Document_Lab7.States;
using Document_Lab7.Colleagues;
using System;
using AppDoc = Document_Lab7.Models.Document;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Document_Lab7.Models
{
    public class Document : Colleague
    {
        public string Title { get; }
        public IDocumentState State { get; private set; }
        public IMediator Mediator { get; set; }

        public Document(string title)
        {
            Title = title;
            State = new NewState();
        }

        public void SetState(IDocumentState state) => State = state;
        public void Print() => State.Print(this);
        public void AddToQueue() => State.AddToQueue(this);
        public void CompletePrinting() => State.CompletePrinting(this);
        public void FailPrinting() => State.FailPrinting(this);
        public void Reset() => State.Reset(this);
    }
}
