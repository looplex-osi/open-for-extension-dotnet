using System;

namespace Looplex.OpenForExtension.Abstractions.Traits
{
    public interface IEventHandlingTrait
    {
        void On(string eventName, EventHandler handler);
        void Off(string eventName, EventHandler handler);
        void Invoke(string eventName, object sender, EventArgs e);
    }
}