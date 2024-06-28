
namespace learningManagementSystem.BLL.DTOs;

public class UpdateCommentReplyDto
{
	[MaxLength(250, ErrorMessage = "The Reply cannot be larger than 250 char")]
	[MinLength(1, ErrorMessage = "The Reply cannot be empty..!!")]
	public string Content { get; set; } = string.Empty;
}
