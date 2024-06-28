

namespace learningManagementSystem.BLL.DTOs;

public class UploadVideoDto
{
	public string Url { get; set; } = string.Empty;

	[MaxLength(100, ErrorMessage = "The video name cannot be over 100 char")]
	[MinLength(1, ErrorMessage = "The video name cannot be empty..!!")]
	public string Title { get; set; } = string.Empty;
	public string Length { get; set; } = string.Empty;

	[MaxLength(250, ErrorMessage = "The video name cannot be over 250 char")]
	[MinLength(1, ErrorMessage = "The vide name cannot be empty...!!")]
	public string Description { get; set; } = string.Empty;

	[MaxLength(20, ErrorMessage = "The video format cannot be over 20 char")]
	[MinLength(1, ErrorMessage = "The vide format cannot be empty...!!")]
	public string FileFormat { get; set; } = string.Empty;
	public bool IsLocked { get; set; } = true;
	public DateTime UploadDate { get; set; } = DateTime.Now;
	public Guid LessonId { get; set; }

}
