

namespace learningManagementSystem.BLL.DTOs;

public class BlockUserDto
{
    [EmailAddress(ErrorMessage = "This is not valid email address..!!")]
    public string Email { get; set; } = string.Empty;
    public int NumberOfDays { get; set; }
    public bool IsForever { get; set; }
}
