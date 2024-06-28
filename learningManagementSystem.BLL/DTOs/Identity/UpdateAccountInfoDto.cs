

namespace learningManagementSystem.BLL.DTOs;

public class UpdateAccountInfoDto
{
	[MaxLength(80, ErrorMessage = "The Name cannot be over 80 char")]
	[MinLength(1, ErrorMessage = "Please enter at least one char")]
	public string DisplayName { get; set; } = string.Empty;

	[MaxLength(80, ErrorMessage = "The UserName cannot be over 80 char")]
	[MinLength(1, ErrorMessage = "Please UserName at least one char")]
	public string UserName { get; set; } = string.Empty;
	public string AvatarUrl { get; set; } = string.Empty;
    public Address Address { get; set; } = new();
}
