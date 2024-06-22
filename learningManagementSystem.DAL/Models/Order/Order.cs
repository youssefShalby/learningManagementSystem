
namespace learningManagementSystem.DAL.Models;

public class Order : BaseModel<Guid>
{
    public string BuyerEmail { get; set; } = string.Empty;
    public decimal Price { get; set; }

    [ForeignKey(nameof(Course))]
    public Guid CourseId { get; set; }
    public Course? Course { get; set; }

	[ForeignKey(nameof(Student))]
	public Guid StudentId { get; set; }
	public Student? Student { get; set; }

	public string PaymentIntentId { get; set; } = string.Empty;
    public string CleintSecret { get; set; } = string.Empty;
}
