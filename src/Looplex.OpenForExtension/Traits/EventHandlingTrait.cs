using System;
using System.Collections.Generic;
using System.Linq;

namespace Looplex.OpenForExtension.Traits
{
    public class EventHandlingTrait
    {
        private readonly IList<string> _events;
        private readonly IDictionary<string, EventHandler> _handlers = new Dictionary<string, EventHandler>();
        private object value;

        public EventHandlingTrait(string[] events)
        {
            _events = events.ToList();
        }

        public void Add(string eventName, EventHandler handler)
        {
            AssertEventExists(eventName);
            if (_handlers.ContainsKey(eventName))
            {
                _handlers[eventName] += handler;
            }
            else
            {
                _handlers[eventName] = handler;
            }
        }

        private void AssertEventExists(string eventName)
        {
            if (!_events.Contains(eventName)) 
            { 
                throw new ArgumentException($"Event {eventName} doens't exist");
            }
        }

        public void Remove(string eventName, EventHandler handler)
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
