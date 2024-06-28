
namespace learningManagementSystem.BLL.DTOs;

public class UpdateVideoDto
{
	[MinLength(1, ErrorMessage = "The video name cannot be empty..!!")]
	public string Title { get; set; } = string.Empty;

	[MaxLength(250, ErrorMessage = "The video name cannot be over 250 char")]
	[MinLength(1, ErrorMessage = "The vide name cannot be empty...!!")]
	public string Description { get; set; } = string.Empty;
	public bool IsLocked { get; set; }

}
