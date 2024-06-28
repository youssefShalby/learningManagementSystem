
namespace learningManagementSystem.BLL.DTOs;

public class UpdateCommentDto
{
	[MaxLength(250, ErrorMessage = "The comment cannot be larger than 250 char")]
	[MinLength(1, ErrorMessage = "The commnet cannot be empty..!!")]
	public string Content { get; set; } = string.Empty;
}
