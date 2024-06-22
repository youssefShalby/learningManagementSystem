

namespace learningManagementSystem.DAL.Models;

public class Instructor : BaseModel<Guid>
{
	[ForeignKey(nameof(AppUser))]
	public string? UserRefId { get; set; } 
    public ApplicationUser? AppUser { get; set; }
    public ICollection<Course>? Courses { get; set; }
}
