

namespace learningManagementSystem.DAL.Models;

public class Video : BaseModel<Guid>
{
    public string Url { get; set; } = string.Empty;
    public string Title  { get; set; } = string.Empty;
    public string Length  { get; set; } = string.Empty;
    public string Description  { get; set; } = string.Empty;
    public string FileFormat  { get; set; } = string.Empty;
    public bool IsLocked { get; set; }
    public DateTime UploadDate { get; set; } = DateTime.Now;


    [ForeignKey(nameof(Lesson))]
    public Guid LessonId { get; set; }
    public Lesson? Lesson { get; set; }

    public ICollection<Comment>? Comments { get; set; }
}
