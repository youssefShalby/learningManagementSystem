

namespace learningManagementSystem.BLL.DTOs;

public class CreateCommentForVideoDto
{
	public string Content { get; set; } = string.Empty;
	public string? UserId { get; set; }
	public Guid? VideoId { get; set; }
}
