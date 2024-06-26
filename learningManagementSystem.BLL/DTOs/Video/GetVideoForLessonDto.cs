

namespace learningManagementSystem.BLL.DTOs;

public class GetVideoForLessonDto
{
	public string Url { get; set; } = string.Empty;
	public string Title { get; set; } = string.Empty;
	public string Length { get; set; } = string.Empty;
	public bool IsLocked { get; set; }
	public DateTime UploadDate { get; set; } = DateTime.Now;
}
