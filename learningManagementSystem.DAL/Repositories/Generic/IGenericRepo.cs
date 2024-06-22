

namespace learningManagementSystem.DAL.Repositories;

public interface IGenericRepo<T> where T : class
{
	Task<IReadOnlyList<T>> GetAllAsync(int pageNumber);
	Task<IEnumerable<T>> GetAllWithIncludesAsync(int pageNumber, params Expression<Func<T, object>>[] includes);
	Task<T> GetByIdAsync<Id>(Id id);
	Task CreateAsync(T entity);
	Task CreateRangeAsync(IEnumerable<T> entities);
	void Delete(T entity);
	void DeleteRange(IEnumerable<T> entities);
	void Update(T entity);
	void UpdateRange(IEnumerable<T> entities);
	Task<int> SaveChangesAsync();
	int SaveChanges();
}
