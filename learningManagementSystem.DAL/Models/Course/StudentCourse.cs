
namespace learningManagementSystem.DAL.Models;

public class StudentCourse
{
    [ForeignKey(nameof(Student))]
    public Guid StudentId { get; set; }
    public Student? Student { get; set; }


	[ForeignKey(nameof(Course))]
	public Guid CourseId { get; set; }
	public Course? Course { get; set; }

    public DateTime AssignedAt { get; set; } = DateTime.Now;
}
