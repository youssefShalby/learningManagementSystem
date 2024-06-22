
namespace learningManagementSystem.DAL.Models;

public class Order : BaseModel<Guid>
{
    public string BuyerEmail { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public Guid CourseId { get; set; }
    public Course? Course { get; set; }

    public string PaymentIntentId { get; set; } = string.Empty;
    public string CleintSecret { get; set; } = string.Empty;
}
