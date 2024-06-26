

namespace learningManagementSystem.BLL.DTOs;

public class CreateOrUpdatePaymentDto
{
    [EmailAddress]
    public string Email { get; set; } = string.Empty;
    public Guid CoursePaymentId { get; set; }

}
