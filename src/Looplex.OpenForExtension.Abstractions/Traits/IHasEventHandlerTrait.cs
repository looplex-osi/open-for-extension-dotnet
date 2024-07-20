using System;

namespace Looplex.OpenForExtension.Abstractions.Traits
{
    public interface IHasEventHandlerTrait
    {
        IEventHandlingTrait EventHandling { get; }
        void On(string eventName, EventHandler eventHandler);
    }
}
