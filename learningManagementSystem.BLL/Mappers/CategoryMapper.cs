

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
			Courses = model.Courses?.Select(course => new GetCourseWithCategoryDto
			{
				Description = course.Description,
				ImgeUrl = course.ImgeUrl,
				OfferOrice = course.OfferOrice,
				OriginalOrice = course.OriginalOrice,
				StudentsNumber = course.StudentsNumber,
				Title = course.Title

			}).ToList()
		};
	}
}
