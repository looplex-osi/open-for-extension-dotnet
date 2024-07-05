namespace TheTortoiseAndTheHare.Entities
{
    internal abstract class BaseEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
    }
}
