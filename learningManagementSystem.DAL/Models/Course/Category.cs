

namespace learningManagementSystem.DAL.Models;

public class Category : BaseModel<Guid>
{
    public string Name { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public bool IsDeleted { get; set; }
    public ICollection<Course>? Courses { get; set; }
}
