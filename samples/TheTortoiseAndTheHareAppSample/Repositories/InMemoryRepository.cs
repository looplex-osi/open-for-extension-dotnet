using TheTortoiseAndTheHare.Entities;

namespace TheTortoiseAndTheHare.Repositories
{
    internal class InMemoryRepository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly List<T> _entities = [];

        public T? GetById(Guid id)
        {
            return _entities.FirstOrDefault(e => e.Id == id);
        }

        public void Add(T entity)
        {
            _entities.Add(entity);
        }
    }
}
