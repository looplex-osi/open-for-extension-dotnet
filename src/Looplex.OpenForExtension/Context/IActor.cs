using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Looplex.OpenForExtension.Context
{
    public interface IObservableActor
    {
        // Method to invoke the event by name
        public void On(string eventName)
        {
            //if (_events.TryGetValue(eventName, out var eventHandler))
            //{
            //    eventHandler?.Invoke(this, EventArgs.Empty);
            //}
        }
        void Notify(IMessage message);
        Task Act(IDefaultContext context);
    }
}
