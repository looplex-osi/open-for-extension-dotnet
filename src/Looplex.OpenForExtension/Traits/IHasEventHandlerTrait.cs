using System;

namespace Looplex.OpenForExtension.Traits
{
    public interface IHasEventHandlerTrait
    {
        EventHandlingTrait EventHandling { get; }
        void On(string eventName, EventHandler eventHandler);
    }
}
