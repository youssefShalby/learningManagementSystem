

namespace learningManagementSystem.DAL.Models;

public class CoursePayment : BaseModel<Guid>
{
    public Guid CourseId { get; set; }
	public string PaymentIntentId { get; set; } = string.Empty;
	public string PaymentClientSecret { get; set; } = string.Empty;
}
