using TheTortoiseAndTheHareAppSample.Domain.Entities;

namespace TheTortoiseAndTheHareAppSample.Domain.Repositories
{
    public interface IRepository<T> where T : BaseEntity
    {
        T? GetById(Guid id);
        void Add(T entity);
    }
}
