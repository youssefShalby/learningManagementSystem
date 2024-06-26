
namespace learningManagementSystem.BLL.DTOs;

public class CreateCourseDto
{
	public string Title { get; set; } = string.Empty;
	public string Description { get; set; } = string.Empty;
	public string Details { get; set; } = string.Empty;
	public decimal OriginalOrice { get; set; }
	public string? ImgeUrl { get; set; }
	public decimal OfferOrice { get; set; }
	public Guid? CategoryId { get; set; }
	public Guid? InstructorId { get; set; }
	public DateTime CreatedAt { get; set; }
	public List<string>? Advanteges { get; set; }
}
