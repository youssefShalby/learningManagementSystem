
namespace learningManagementSystem.BLL.DTOs;

public class CreateCategoryDto
{
	[MaxLength(100, ErrorMessage = "The name cannot be larger than 100 char")]
	[MinLength(1, ErrorMessage = "The name cannot be empty..!!")]
	public string Name { get; set; } = string.Empty;
	public DateTime CreatedAt { get; set; }
}
