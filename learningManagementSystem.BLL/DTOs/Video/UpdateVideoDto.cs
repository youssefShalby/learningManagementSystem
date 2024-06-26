
namespace learningManagementSystem.BLL.DTOs;

public class UpdateVideoDto
{
	public string Title { get; set; } = string.Empty;
	public string Description { get; set; } = string.Empty;
	public bool IsLocked { get; set; }

}
