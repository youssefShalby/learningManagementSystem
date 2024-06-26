

namespace learningManagementSystem.BLL.DTOs;

public class GetLessonToCourseDto
{
	public string Name { get; set; } = string.Empty;
	public int VideosNumber { get; set; }
	public int Duration { get; set; }
	public Guid CourseId { get; set; }
    public List<GetVideoForLessonDto>? Videos { get; set; }
}
