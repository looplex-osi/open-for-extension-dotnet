using Looplex.OpenForExtension.Traits;

namespace TheTortoiseAndTheHare.Entities
{
    internal abstract class Racer : BaseEntity, IHasEventHandlerTrait
    {        
        private static readonly string StartedToRun = "StartedToRun";
        private static readonly string IsExausted = "IsExausted";
        private static readonly string FinishedTheRace = "FinishedTheRace";

        public required string Name { get; set; }
        public int Speed { get; set; }
        public int Endurance { get; set; }
        public string? State { get; set; }

        public int Position { get; set; }
        public int Distance { get; set; }

        public EventHandlingTrait EventHandling { get; } = new([StartedToRun, IsExausted, FinishedTheRace]);

        public void StartTheRace(int distance)
        {
            Position = 0;
            Distance = distance;

            State = "Running";
            OnStartedToRun();
        }

        public void Sprint()
        {
            if (State == "Stopped")
            {
                return;
            }

            if (Endurance <= 0)
            {
                State = "Sleeping";
                OnIsExausted();
            }
            if (State == "Sleeping")
            {
                if (Endurance < 50)
                {
                    Endurance += 10;
                }
                else 
                {
                    State = "Running";
                }
                Console.WriteLine($"Racer {Name} is sleeping.");
            }
            else
            {
                if (Position < Distance)
                {
                    Position += Speed;
                    Endurance -= Speed;
                    Console.WriteLine($"Racer {Name} position is {Position}.");

                };
                if (Position >= Distance)
                {
                    OnFinishedTheRace();
                    State = "Stopped";
                };
            }            
        }

        protected virtual void OnStartedToRun()
        {
            EventHandling.Invoke(StartedToRun, this, EventArgs.Empty);
        }

        protected virtual void OnIsExausted()
        {
            EventHandling.Invoke(IsExausted, this, EventArgs.Empty);
        }

        protected virtual void OnFinishedTheRace()
        {
            EventHandling.Invoke(FinishedTheRace, this, EventArgs.Empty);
        }

        public void On(string eventName, EventHandler eventHandler) => 
            EventHandling.On(eventName, eventHandler);

    }
}
