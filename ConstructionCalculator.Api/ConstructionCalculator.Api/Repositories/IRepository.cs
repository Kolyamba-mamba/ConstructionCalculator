using System;
using System.Linq;
using System.Threading.Tasks;

namespace ConstructionCalculator.Api.Repositories
{
    public interface IRepository<T> where T : class
    {
        // Получение списка
        IQueryable<T> Get();
        //Получение одного элемента
        Task<T> GetById(Guid id);
        //Изменение элемента
        Task Update(T item);
        //Удаление элемента
        Task Delete(Guid id);
        //Создание элемента
        Task Create(T item);
    }
}
