

namespace learningManagementSystem.DAL.Models;

public class ApplicationUser : IdentityUser
{
    public string DisplayName { get; set; } = string.Empty;
    public string Avatar { get; set; } = string.Empty;
    public Address Address { get; set; } = new();
    public ICollection<StudentCourse>? UserCourses { get; set; }

}
