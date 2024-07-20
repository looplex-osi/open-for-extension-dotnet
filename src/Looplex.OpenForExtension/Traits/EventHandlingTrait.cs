using System;
using System.Collections.Generic;
using System.Linq;
using Looplex.OpenForExtension.Abstractions.Traits;

namespace Looplex.OpenForExtension.Traits
{
    public class EventHandlingTrait : IEventHandlingTrait
    {
        private readonly IList<string> _events;
        private readonly IDictionary<string, EventHandler> _handlers = new Dictionary<string, EventHandler>();

        public EventHandlingTrait(string[] events)
        {
            _events = events.ToList();
        }

        public void On(string eventName, EventHandler eventHandler)
        {
            AssertEventExists(eventName);
            if (!_handlers.ContainsKey(eventName))
            {
                _handlers[eventName] = eventHandler;
            }
            else 
            { 
                _handlers[eventName] += eventHandler;
            }
        }

        private void AssertEventExists(string eventName)
        {
            if (!_events.Contains(eventName)) 
            { 
                throw new ArgumentException($"Event {eventName} doens't exist");
            }
        }

        public void Off(string eventName, EventHandler handler)
        {
            AssertEventExists(eventName);
            if (_handlers.ContainsKey(eventName))
            {
                _handlers[eventName] -= handler;
                if (_handlers[eventName] == null)
                {
                    _handlers.Remove(eventName);
                }
            }
        }

        public void Invoke(string eventName, object sender, EventArgs e)
        {
            AssertEventExists(eventName);
            if (_handlers.ContainsKey(eventName))
            {
                _handlers[eventName].Invoke(sender, e);
            }
        }
    }
}
