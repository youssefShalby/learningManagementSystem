
namespace learningManagementSystem.BLL.DTOs;

public class AddRoleDto
{
    [MaxLength(100, ErrorMessage = "Role name cannot be over 100 char")]
    [MinLength(1, ErrorMessage = "Role name cannot be empty")]
    public string Name { get; set; } = string.Empty;
}
