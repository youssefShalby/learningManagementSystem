

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
	
	public static GetCategoryByIdWithIncludesDto ToCategoryWithCourseDto(Category model)
	{
		return new GetCategoryByIdWithIncludesDto
		{
			Name = model.Name,
			Courses = model.Courses?.Select(course => CourseMapper.ToGetDto(course)).ToList()
		};
	}
}
