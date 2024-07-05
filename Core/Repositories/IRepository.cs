namespace Core.Repositories;

public interface IRepository<T>
{
    Task<List<T>> Pageable(int number, int size);
    Task<T> Add(T entity);
    Task<IEnumerable<T>> GetAll();
    Task<T> GetById(Guid id);
    Task<T> Update(T entity);
    Task<bool> Remove(Guid id);
    Task<int> Count();
}
