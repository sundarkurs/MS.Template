using System.Linq.Expressions;

namespace WSA.Microservice.Template.Application.Common.Interfaces.Repositories
{
    public interface IBaseRepository<T> where T : class
    {
        Task<T> GetByIdAsync(int id);

        Task<T> GetByIdAsync(Guid id);

        Task<List<T>> GetAllAsync();

        Task<List<T>> GetPagedReponseAsync(int pageNumber, int pageSize);

        Task<T> AddAsync(T entity);

        Task UpdateAsync(T entity);

        Task DeleteAsync(T entity);

        IQueryable<T> GetObjectsQueryable(Expression<Func<T, bool>> predicate, string includeTable = "");
    }
}
