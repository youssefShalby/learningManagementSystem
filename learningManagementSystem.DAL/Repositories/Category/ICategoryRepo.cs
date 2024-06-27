



namespace learningManagementSystem.DAL.Repositories;

public interface ICategoryRepo : IGenericRepo<Category>
{
	Task<IEnumerable<Category>> GetAllByQueryAsync(CategoryQueryHandler query);
	Task<Category> GetByIdWithIncludesAsync(Guid id);
	public int GetCategoriesCount();

	//> Add more method to serve the model here...
}
