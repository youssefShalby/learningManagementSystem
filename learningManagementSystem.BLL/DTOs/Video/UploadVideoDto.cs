

namespace learningManagementSystem.BLL.DTOs;

public class UploadVideoDto
{
	public string Url { get; set; } = string.Empty;
	public string Title { get; set; } = string.Empty;
	public string Length { get; set; } = string.Empty;
	public string Description { get; set; } = string.Empty;
	public string FileFormat { get; set; } = string.Empty;
	public DateTime UploadDate { get; set; } = DateTime.Now;
	public Guid LessonId { get; set; }

}
