

namespace learningManagementSystem.BLL.DTOs;

public class BlockUserDto
{
    public string Email { get; set; } = string.Empty;
    public int NumberOfDays { get; set; }
    public bool IsForever { get; set; }
}
