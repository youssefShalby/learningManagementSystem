

namespace learningManagementSystem.BLL.DTOs;

public class CreateLessonDto
{
	public string Name { get; set; } = string.Empty;
	public Guid CourseId { get; set; }
}
