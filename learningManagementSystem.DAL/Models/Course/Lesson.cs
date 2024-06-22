

namespace learningManagementSystem.DAL.Models;

public class Lesson : BaseModel<Guid>
{
    public string Name { get; set; } = string.Empty;
    public int VideosNumber { get; set; }
    public int Duration { get; set; }
    public bool IsDeleted { get; set; }

    [ForeignKey(nameof(Course))]
    public Guid CourseId { get; set; }
    public Course? Course { get; set; }

    public ICollection<Video>? Videos { get; set; }
}
