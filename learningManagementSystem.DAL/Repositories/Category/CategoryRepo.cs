
namespace learningManagementSystem.DAL.Repositories;

public class CategoryRepo : GenericRepo<Category>, ICategoryRepo
{
	private readonly AppDbContext _context;
	private readonly IConfiguration _configuration;

	public CategoryRepo(AppDbContext context, IConfiguration configuration) : base(context, configuration)
    {
		_context = context;
		_configuration = configuration;
	}

	public async Task<IEnumerable<Category>> GetAllByQueryAsync(CategoryQueryHandler query)
	{
		var categories = _context.Set<Category>().AsNoTracking().AsQueryable();
		query.PageSize = int.Parse(_configuration["CustomConfiguration:CustomConfiguration"]);

		//> Filter
		if (!string.IsNullOrEmpty(query.Name))
		{
			categories = categories.Where(cat => cat.Name.Contains(query.Name));
		}

		//> Sort
		if (!string.IsNullOrEmpty(query.SortBy))
		{
			if (query.SortBy.Equals("Name"))
			{
				categories = query.IsDescending ? categories.OrderByDescending(c => c.Name) : categories.OrderBy(c => c.Name);
			}
		}

		//> pagination
		categories = categories.Skip((query.PageNumber - 1) * query.PageSize).Take(query.PageSize);
		return await categories.ToListAsync();
	}

	public async Task<Category> GetByIdWithIncludesAsync(Guid id)
	{
		return await _context.Set<Category>()
			.Include(c => c.Courses)
			.FirstOrDefaultAsync(c => c.Id == id) ?? null!;		
	}

	//> Implement more method to serve the model here...
}
