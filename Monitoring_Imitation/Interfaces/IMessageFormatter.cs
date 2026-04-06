using Monitoring_Imitation.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monitoring_Imitation.Interfaces
{
    public interface IMessageFormatter
    {
        string Format(MetricEventArgs eventArgs);
    }
}
