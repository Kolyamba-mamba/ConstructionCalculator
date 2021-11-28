using ConstructionCalculator.Api.Helpers.DatabaseHelpers;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ConstructionCalculator.Api.Repositories
{
    public class EntityRepository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationContext _applicationContext;

        public EntityRepository(ApplicationContext applicationContext) {
            this._applicationContext = applicationContext;
        }
        public async Task Update(T item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));
            _applicationContext.Update(item);
            await _applicationContext.SaveChangesAsync();
        }

        public async Task Create(T item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));
            _applicationContext.Add(item);
            await _applicationContext.SaveChangesAsync();
        }

        public async Task Delete(Guid id)
        {
            var entity = _applicationContext.Set<T>().Find(id);
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _applicationContext.Set<T>().Remove(entity);
            await _applicationContext.SaveChangesAsync();
        }

        public IQueryable<T> Get() =>
            _applicationContext.Set<T>().AsQueryable();

        public async Task<T> GetById(Guid id) =>
            await _applicationContext.Set<T>().FindAsync(id);
    }
}
