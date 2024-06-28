

namespace learningManagementSystem.BLL.DTOs;

public class CreateLessonDto
{
	[MaxLength(80, ErrorMessage = "The lesson name cannot be over 80 char")]
	[MinLength(1, ErrorMessage = "The lesson name cannot be empty")]
	public string Name { get; set; } = string.Empty;
	public Guid CourseId { get; set; }
}
