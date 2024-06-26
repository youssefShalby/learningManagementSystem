

namespace learningManagementSystem.BLL.DTOs;

public class BuyCourseDto
{
	public string Title { get; set; } = string.Empty;
	public decimal Price { get; set; }
	public string? ImgeUrl { get; set; }
	public string PaymentIntentId { get; set; } = string.Empty;
	public string PaymentClientSecret { get; set; } = string.Empty;
}
