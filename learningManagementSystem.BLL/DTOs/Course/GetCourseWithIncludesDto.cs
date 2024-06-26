

namespace learningManagementSystem.BLL.DTOs;

public class GetCourseWithIncludesDto
{
	public string Title { get; set; } = string.Empty;
	public string Description { get; set; } = string.Empty;
	public string Details { get; set; } = string.Empty;
	public string CategoryName { get; set; } = string.Empty;
	public string InstructorName { get; set; } = string.Empty;
	public decimal OriginalOrice { get; set; }
	public string? ImgeUrl { get; set; }
	public decimal OfferOrice { get; set; }
	public DateTime CreatedAt { get; set; }
	public int StudentsNumber { get; set; }
    public List<string>? Advanteges { get; set; }

    public List<GetCommentForCourseDto>? Comments { get; set; }
    public List<GetLessonToCourseDto>? Lessons { get; set; }
}
