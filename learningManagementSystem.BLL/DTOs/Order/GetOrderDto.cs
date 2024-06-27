

namespace learningManagementSystem.BLL.DTOs;

public class GetOrderDto
{
	public string BuyerEmail { get; set; } = string.Empty;
	public decimal Price { get; set; }
	public DateTime CreatedAt { get; set; }
	public string CourseName { get; set; } = string.Empty;
	public string StudentEmail { get; set; } = string.Empty;
}
