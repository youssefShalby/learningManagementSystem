
namespace learningManagementSystem.BLL.DTOs;

public class EnrollStudentToCourseDto
{
	[EmailAddress(ErrorMessage = "this is not valid email address")]
	[Required(ErrorMessage = "the email is reqiured")]
	public string Email { get; set; } = string.Empty;
    public Guid CourseId { get; set; }
}
