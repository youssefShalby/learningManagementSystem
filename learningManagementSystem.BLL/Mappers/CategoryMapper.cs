

namespace learningManagementSystem.BLL.Mappers;

public class CategoryMapper
{
	public static GetCategoryToShowDto ToCategoryDto(Category model)
	{
		return new GetCategoryToShowDto
		{
			Name = model.Name,
		};
	}

}
