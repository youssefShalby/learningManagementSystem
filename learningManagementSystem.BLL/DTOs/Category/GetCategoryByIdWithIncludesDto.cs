

namespace learningManagementSystem.BLL.DTOs;

public class GetCategoryByIdWithIncludesDto
{
    public string Name { get; set; } = string.Empty;
    public IEnumerable<GetCourseWithCategoryDto>? Courses { get; set; }
}
