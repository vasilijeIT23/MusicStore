using System.Linq.Expressions;

namespace MusicStoreApi.Repository
{
    public interface IRepository<T>
    {
        IEnumerable<T> Find(Expression<Func<T, bool>> expression);
        IEnumerable<T> GetAll();
        T? GetById(Guid Id);
        void Create(T entity);
        void Delete(T entity);
        bool SaveChanges();

    }

}
