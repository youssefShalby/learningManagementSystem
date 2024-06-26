

namespace learningManagementSystem.BLL.DTOs;

public class CreateOrderDto
{
	public string BuyerEmail { get; set; } = string.Empty;
	public decimal Price { get; set; }
	public Guid CourseId { get; set; }
	public Guid StudentId { get; set; }
	public string PaymentIntentId { get; set; } = string.Empty;
	public string CleintSecret { get; set; } = string.Empty;
}
