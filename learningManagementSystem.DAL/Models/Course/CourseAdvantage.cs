

namespace learningManagementSystem.DAL.Models;

public class CourseAdvantage : BaseModel<Guid>
{
    public string Advantege { get; set; } = string.Empty;

    [ForeignKey(nameof(Course))]
    public Guid CourseId { get; set; }
    public Course? Course { get; set; }
}
