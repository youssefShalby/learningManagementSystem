

namespace learningManagementSystem.BLL.DTOs;

public class GetCourseWithCategoryDto
{
	public string Title { get; set; } = string.Empty;
	public string Description { get; set; } = string.Empty;
	public string? ImgeUrl { get; set; }
	public decimal OriginalOrice { get; set; }
	public decimal OfferOrice { get; set; }
	public int StudentsNumber { get; set; }
}
