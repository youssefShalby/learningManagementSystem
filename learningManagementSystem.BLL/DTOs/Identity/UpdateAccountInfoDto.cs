

namespace learningManagementSystem.BLL.DTOs;

public class UpdateAccountInfoDto
{
    public string DisplayName { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public string AvatarUrl { get; set; } = string.Empty;
    public Address Address { get; set; } = new();
}
