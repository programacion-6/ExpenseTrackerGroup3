namespace ExpenseTrackerGroup3.Repositories.Interfaces;

public interface IRepository<T> where T : class
{
    Task<bool> CreateAsync(T item);
    Task<bool> UpdateAsync(T item);
    Task<bool> DeleteAsync(Guid id);
    Task<T> GetByIdAsync(Guid id);
    Task<IEnumerable<T>> GetAllAsync();
}
