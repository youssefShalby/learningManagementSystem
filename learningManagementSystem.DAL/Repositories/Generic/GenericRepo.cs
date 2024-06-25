

namespace learningManagementSystem.DAL.Repositories;

public class GenericRepo<T> : IGenericRepo<T> where T : class
{
	private readonly AppDbContext _context;
	private readonly IConfiguration _configuration;
	private readonly int pageSize;

	public GenericRepo(AppDbContext context, IConfiguration configuration)
    {
		_context = context;
		_configuration = configuration;
		pageSize = int.Parse(_configuration["CustomConfiguration:PageSize"]);
	}
    public async Task CreateAsync(T entity)
	{
		await _context.Set<T>().AddAsync(entity);
	}

	public async Task CreateRangeAsync(IEnumerable<T> entities)
	{
		await _context.Set<T>().AddRangeAsync(entities);
	}

	public void Delete(T entity)
	{
		_context.Set<T>().Remove(entity);
	}

	public void DeleteRange(IEnumerable<T> entities)
	{
		_context.Set<T>().RemoveRange(entities);
	}

	public async Task<IReadOnlyList<T>> GetAllAsync(int pageNumber)
	{
		return await _context.Set<T>()
			.AsNoTracking()
			.ToListAsync();
	}

	public async Task<IEnumerable<T>> GetAllWithIncludesAsync(int pageNumber, params Expression<Func<T, object>>[] includes)
	{
		var query = _context.Set<T>().AsQueryable();
		foreach (var include in includes)
		{
			query.Include(include);
		}

		return await query.Skip((pageNumber - 1) * pageSize)
			.Take(pageSize)
			.ToListAsync();
	}

	public async Task<T> GetByIdAsync<Id>(Id id)
	{
		return await _context.Set<T>().FindAsync(id) ?? null!;
	}

	public int SaveChanges()
	{
		return _context.SaveChanges();
	}

	public async Task<int> SaveChangesAsync()
	{
		return await _context.SaveChangesAsync();
	}

	public void Update(T entity)
	{
		_context.Set<T>().Update(entity);
	}

	public void UpdateRange(IEnumerable<T> entities)
	{
		_context.Set<T>().UpdateRange(entities);
	}
}
