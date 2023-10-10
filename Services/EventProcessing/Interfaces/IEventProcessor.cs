using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.EventProcessing.Interfaces
{
    public interface IEventProcessor
    {
        void ProcessEvent(string message);
    }
}
