using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monitoring_Imitation.Interfaces
{
    public interface INotificationChannel
    {
        string Name { get; }
        void Send(string message);
    }
}
