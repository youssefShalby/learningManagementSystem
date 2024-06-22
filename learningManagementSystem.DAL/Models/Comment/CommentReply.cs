

namespace learningManagementSystem.DAL.Models;

public class CommentReply : BaseModel<Guid>
{
	public string Content { get; set; } = string.Empty;
	public DateTime CreatedAt { get; set; }


	[ForeignKey(nameof(AppUser))]
	public string? UserId { get; set; }
	public ApplicationUser? AppUser { get; set; }

    public Guid CommentId { get; set; }
    public Comment? Comment{ get; set; }
}
