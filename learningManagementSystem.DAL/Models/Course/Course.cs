namespace learningManagementSystem.DAL.Models;

public class Course : BaseModel<Guid>
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Details { get; set; } = string.Empty;
    public decimal OriginalOrice { get; set; }
    public string? ImgeUrl { get; set; }
    public decimal OfferOrice { get; set; }
    public DateTime CreatedAt { get; set; }
    public bool IsDeleted { get; set; }
    public int StudentsNumber { get; set; }

	[ForeignKey(nameof(Category))]
    public Guid? CategoryId { get; set; }
    public Category? Category { get; set; }


	[ForeignKey(nameof(Instructor))]
	public Guid? InstructorId { get; set; }
	public Instructor? Instructor { get; set; }

	public ICollection<CourseAdvantage>? Advanteges { get; set; }
    public ICollection<Comment>? Comments { get; set; }
    public ICollection<Lesson>? Lessons { get; set; }
    public ICollection<StudentCourse>? StudentCourses { get; set; }
    public ICollection<CoursePayment>? CoursePayments { get; set; }
}
