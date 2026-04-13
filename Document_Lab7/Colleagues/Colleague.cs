using Document_Lab7.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Document_Lab7.Colleagues
{
    public abstract class Colleague
    {
        protected IMediator Mediator;
        public void SetMediator(IMediator mediator)
        {
            Mediator = mediator;
        }
    }
}
