
namespace learningManagementSystem.BLL.DTOs;

public class CreateCommentReplyDto
{
	public string Content { get; set; } = string.Empty;

	[JsonIgnore]
	public string? UserId { get; set; }
	public Guid CommentId { get; set; }
}
