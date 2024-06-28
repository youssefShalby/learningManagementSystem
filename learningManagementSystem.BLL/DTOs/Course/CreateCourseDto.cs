
namespace learningManagementSystem.BLL.DTOs;

public class CreateCourseDto
{
	[MaxLength(100, ErrorMessage = "The course title cannot be larger than 100 char")]
	[MinLength(1, ErrorMessage = "The course title cannot be empty..!!")]
	public string Title { get; set; } = string.Empty;

	[MaxLength(150, ErrorMessage = "The course description cannot be larger than 150 char")]
	[MinLength(1, ErrorMessage = "The course description cannot be empty..!!")]
	public string Description { get; set; } = string.Empty;

	[MaxLength(250, ErrorMessage = "The course Details cannot be larger than 250 char")]
	[MinLength(1, ErrorMessage = "The course Details cannot be empty..!!")]
	public string Details { get; set; } = string.Empty;
	public decimal OriginalOrice { get; set; }
	public string? ImgeUrl { get; set; }
	public decimal OfferOrice { get; set; }
	public Guid? CategoryId { get; set; }

	[JsonIgnore]
	public Guid? InstructorId { get; set; }
	public DateTime CreatedAt { get; set; }
	public List<string>? Advanteges { get; set; }
}
