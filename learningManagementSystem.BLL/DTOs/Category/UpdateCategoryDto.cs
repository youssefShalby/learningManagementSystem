

namespace learningManagementSystem.BLL.DTOs;

public class UpdateCategoryDto
{
	[Required(ErrorMessage = "The name is required...!!")]
	[MaxLength(100, ErrorMessage = "The name cannot be larger than 100 char")]
	public string Name { get; set; } = string.Empty;
}
