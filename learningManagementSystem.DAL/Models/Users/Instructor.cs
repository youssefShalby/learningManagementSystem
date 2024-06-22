

namespace learningManagementSystem.DAL.Models;

public class Instructor : BaseModel<Guid>
{
    public Guid UserRefId { get; set; }
    public ApplicationUser? AppUser { get; set; }
    public ICollection<Course>? Courses { get; set; }
}
