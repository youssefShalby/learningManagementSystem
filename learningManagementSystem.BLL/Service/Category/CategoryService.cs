




namespace learningManagementSystem.BLL.Service;

public class CategoryService : ICategoryService
{
	private readonly IUnitOfWork _unitOfWork;

	public CategoryService(IUnitOfWork unitOfWork)
    {
		_unitOfWork = unitOfWork;
	}

	public async Task<CommonResponse> CreateCategoryAsync(CreateCategoryDto model)
	{
		try
		{
			var newCategory = new Category
			{
				CreatedAt = DateTime.Now,
				Name = model.Name,
				Id = Guid.NewGuid(),
			};

			await _unitOfWork.CategoryRepo.CreateAsync(newCategory);
			await _unitOfWork.CategoryRepo.SaveChangesAsync();
			return new CommonResponse("category created..!!", true);
		}
		catch(Exception ex) 
		{
			return new CommonResponse($"cannot created because {ex.Message}", true);
		}
	}

	public async Task<CommonResponse> DeleteCategoryAsync(Guid id)
	{
		try
		{
			var categoryToDelete = await _unitOfWork.CategoryRepo.GetByIdAsync(id);
			if (categoryToDelete is null)
			{
				return new CommonResponse("category not founded..!!", false);
			}

			_unitOfWork.CategoryRepo.Delete(categoryToDelete);
			_unitOfWork.CategoryRepo.SaveChanges();

			return new CommonResponse("category Deleted..!!", true);
		}
		catch (Exception ex)
		{
			return new CommonResponse($"cannot Delete because {ex.Message}", true);
		}
	}

	public async Task<IEnumerable<GetCategoryToShowDto>> GetAllAsync(int pageNumber)
	{
		var catgories = await _unitOfWork.CategoryRepo.GetAllAsync(pageNumber);
		if(catgories is null)
		{
			return new List<GetCategoryToShowDto>();
		}

		catgories = catgories.Where(cat => cat.IsDeleted == false).ToList();

		return catgories.Select(cat => CategoryMapper.ToCategoryDto(cat));
	}


	public async Task<IEnumerable<GetCategoryToShowDto>> GetAllByQueryAsync(CategoryQueryHandler query)
	{
		var catgories = await _unitOfWork.CategoryRepo.GetAllByQueryAsync(query);
		if (catgories is null)
		{
			return new List<GetCategoryToShowDto>();
		}

		catgories = catgories.Where(cat => cat.IsDeleted == false);

		return catgories.Select(cat => CategoryMapper.ToCategoryDto(cat));
	}

	public async Task<GetCategoryByIdWithIncludesDto> GetByIdWithIncludesAsync(Guid id)
	{
		var category = await _unitOfWork.CategoryRepo.GetByIdWithIncludesAsync(id);
		if(category is null || category.IsDeleted)
		{
			return new GetCategoryByIdWithIncludesDto();
		}

		return CategoryMapper.ToCategoryWithCourseDto(category);
	}

	public async Task<CommonResponse> MarkCategoryAsDeletedAsync(Guid id)
	{
		try
		{
			var categoryToUpdate = await _unitOfWork.CategoryRepo.GetByIdAsync(id);
			if (categoryToUpdate is null || categoryToUpdate.IsDeleted)
			{
				return new CommonResponse("category not founded..!!", false);
			}

			categoryToUpdate.IsDeleted = true;
			_unitOfWork.CategoryRepo.Update(categoryToUpdate);
			_unitOfWork.CategoryRepo.SaveChanges();

			return new CommonResponse("category Deleted..!!", true);
		}
		catch (Exception ex)
		{
			return new CommonResponse($"cannot Delete because {ex.Message}", true);
		}
	}

	public async Task<CommonResponse> UpdateCategoryAsync(Guid id, UpdateCategoryDto model)
	{
		try
		{
			var categoryToUpdate = await _unitOfWork.CategoryRepo.GetByIdAsync(id);
			if(categoryToUpdate is null || categoryToUpdate.IsDeleted)
			{
				return new CommonResponse("category not founded..!!", false);
			}

			categoryToUpdate.Name = model.Name;
			_unitOfWork.CategoryRepo.Update(categoryToUpdate);
			_unitOfWork.CategoryRepo.SaveChanges();

			return new CommonResponse("category updated..!!", true);
		}
		catch (Exception ex)
		{
			return new CommonResponse($"cannot update because {ex.Message}", true);
		}

	}
}
