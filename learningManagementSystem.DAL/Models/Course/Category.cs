

namespace learningManagementSystem.DAL.Models;

public class Category : BaseModel<Guid>
{
    public string Name { get; set; } = string.Empty;
    public ICollection<Course>? Courses { get; set; }
}
