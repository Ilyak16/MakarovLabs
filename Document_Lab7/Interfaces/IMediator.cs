using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using AppDoc = Document_Lab7.Models.Document;
using Document_Lab7.Colleagues;

namespace Document_Lab7.Interfaces
{
    public interface IMediator
    {
        void Notify(Colleague sender, string ev, AppDoc document = null);
    }
}
