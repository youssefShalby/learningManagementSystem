

namespace learningManagementSystem.BLL.Service;

public interface ICategoryService
{
	Task<IEnumerable<GetCategoryToShowDto>> GetAllAsync(int pageNumber);
	Task<IEnumerable<GetCategoryToShowDto>> GetAllByQueryAsync(CategoryQueryHandler query);
	Task<GetCategoryByIdWithIncludesDto> GetByIdWithIncludesAsync(Guid id);
	Task<CommonResponse> CreateCategoryAsync(CreateCategoryDto model);
	Task<CommonResponse> UpdateCategoryAsync(Guid id, UpdateCategoryDto model);
	Task<CommonResponse> DeleteCategoryAsync(Guid id);
	Task<CommonResponse> MarkCategoryAsDeletedAsync(Guid id);
}
