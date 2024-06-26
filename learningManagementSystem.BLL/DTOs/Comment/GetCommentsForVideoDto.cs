
namespace learningManagementSystem.BLL.DTOs;

public class GetCommentsForVideoDto
{
	public string Content { get; set; } = string.Empty;
	public string UserName { get; set; } = string.Empty;
	public DateTime CreatedAt { get; set; }
	public string? UserId { get; set; }

	//-- load replies when click on comments --
	//public List<GetRepliesDto>? Replies { get; set; }
}
