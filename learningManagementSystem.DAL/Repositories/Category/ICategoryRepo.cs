



namespace learningManagementSystem.DAL.Repositories;

public interface ICategoryRepo : IGenericRepo<Category>
{
	Task<IEnumerable<Category>> GetAllByQueryAsync(CategoryQueryHandler query);
	Task<Category> GetByIdWithIncludesAsync(Guid id);

	//> Add more method to serve the model here...
}
