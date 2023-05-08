using Microsoft.EntityFrameworkCore;
using MusicStoreInfrastructure;
using System.Linq.Expressions;

namespace MusicStoreApi.Repository
{
    public abstract class Repository<T> : IRepository<T> where T : class
    {
        internal MusicStoreContext context;
        internal DbSet<T> dbSet;

        public Repository(MusicStoreContext context)
        {
            this.context = context;
            this.dbSet = context.Set<T>();
        }

        public virtual IEnumerable<T> GetAll() => context.Set<T>().AsEnumerable();

        public virtual IEnumerable<T> Find(Expression<Func<T, bool>> predicate) => dbSet.Where(predicate).AsEnumerable();

        public virtual T? GetById(Guid id) => dbSet.Find(id);

        public virtual void Insert(T entity) => dbSet.Add(entity);

        public virtual void Delete(T entity) => dbSet.Remove(entity);
    }

}
