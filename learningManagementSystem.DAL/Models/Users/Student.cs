

namespace learningManagementSystem.DAL.Models;

public class Student : BaseModel<Guid>
{
	public Guid UserRefId { get; set; }
	public ApplicationUser? AppUser { get; set; }
    public ICollection<StudentCourse>? StudentCourses { get; set; }
}
