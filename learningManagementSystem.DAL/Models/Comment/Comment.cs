namespace learningManagementSystem.DAL.Models;

public class Comment : BaseModel<Guid>
{
	public string Content { get; set; } = string.Empty;
	public DateTime CreatedAt { get; set; } = DateTime.Now;


	[ForeignKey(nameof(AppUser))]
	public string? UserId { get; set; }
	public ApplicationUser? AppUser { get; set; }


	[ForeignKey(nameof(Course))]
    public Guid? CourseId { get; set; }
    public Course? Course { get; set; }


	[ForeignKey(nameof(Video))]
	public Guid? VideoId { get; set; }
	public Video? Video { get; set; }


    public ICollection<CommentReply>? Replies { get; set; }

}
