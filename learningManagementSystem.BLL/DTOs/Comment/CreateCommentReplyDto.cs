
namespace learningManagementSystem.BLL.DTOs;

public class CreateCommentReplyDto
{
	[MaxLength(250, ErrorMessage = "The Reply cannot be larger than 250 char")]
	[MinLength(1, ErrorMessage = "The Reply cannot be empty..!!")]
	public string Content { get; set; } = string.Empty;

	[JsonIgnore]
	public string? UserId { get; set; }
	public Guid CommentId { get; set; }
}
