

namespace learningManagementSystem.BLL.DTOs;

public class GetCourseToAdminDashDto
{
	public string Title { get; set; } = string.Empty;
	public string Description { get; set; } = string.Empty;
	public decimal OriginalOrice { get; set; }
	public decimal OfferOrice { get; set; }
	public int StudentsNumber { get; set; }
	public string InstructorEmail { get; set; } = string.Empty;
}
