
namespace learningManagementSystem.BLL.DTOs;

public class EnrollStudentToCourseDto
{
    public string Email { get; set; } = string.Empty;
    public Guid CourseId { get; set; }
}
