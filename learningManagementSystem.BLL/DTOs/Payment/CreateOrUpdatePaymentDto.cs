

namespace learningManagementSystem.BLL.DTOs;

public class CreateOrUpdatePaymentDto
{
    [JsonIgnore]
    public string Email { get; set; } = string.Empty;
    public Guid CoursePaymentId { get; set; }

}
