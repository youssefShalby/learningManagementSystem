
namespace learningManagementSystem.BLL.DTOs;

public class CreateCommentForCourseDto
{
	public string Content { get; set; } = string.Empty;
	public string? UserId { get; set; }
	public Guid CourseId { get; set; }

}
