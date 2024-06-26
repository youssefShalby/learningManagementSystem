
namespace learningManagementSystem.BLL.DTOs;

public class GetRepliesDto
{
	public string Content { get; set; } = string.Empty;
	public string UserName { get; set; } = string.Empty;
	public DateTime CreatedAt { get; set; }
	public string? UserId { get; set; }
}
