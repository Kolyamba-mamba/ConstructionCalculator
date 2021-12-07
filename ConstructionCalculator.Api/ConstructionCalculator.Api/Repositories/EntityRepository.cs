using ConstructionCalculator.Api.Helpers.DatabaseHelpers;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ConstructionCalculator.Api.Repositories
{
    /// <summary>
    /// Репозиторий для операций над сущностями
    /// </summary>
    public class EntityRepository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationContext _applicationContext;

        /// <summary>
        /// Конструктор
        /// </summary>
        public EntityRepository(ApplicationContext applicationContext) {
            this._applicationContext = applicationContext;
        }
        /// <summary>
        /// Обновление
        /// </summary>
        /// <param name="item">Обновляемая запись</param>
        /// <returns></returns>
        public async Task Update(T item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));
            _applicationContext.Update(item);
            await _applicationContext.SaveChangesAsync();
        }
        /// <summary>
        /// Создание
        /// </summary>
        /// <param name="item">Создаваемая запись</param>
        /// <returns></returns>
        public async Task Create(T item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));
            _applicationContext.Add(item);
            await _applicationContext.SaveChangesAsync();
        }
        /// <summary>
        /// Удаление
        /// </summary>
        /// <param name="id">Идентификатор удаляемой записи</param>
        /// <returns></returns>
        public async Task Delete(Guid id)
        {
            var entity = _applicationContext.Set<T>().Find(id);
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _applicationContext.Set<T>().Remove(entity);
            await _applicationContext.SaveChangesAsync();
        }
        /// <summary>
        /// Получение списка
        /// </summary>
        /// <returns>Список записей</returns>
        public IQueryable<T> Get() =>
            _applicationContext.Set<T>().AsQueryable();
        /// <summary>
        /// Получение записи по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор записи</param>
        /// <returns></returns>
        public async Task<T> GetById(Guid id) =>
            await _applicationContext.Set<T>().FindAsync(id);
    }
}
