using TheTortoiseAndTheHare.Entities;

namespace TheTortoiseAndTheHare.Repositories
{
    internal interface IRepository<T> where T : BaseEntity
    {
        T? GetById(Guid id);
        void Add(T entity);
    }
}
