

namespace learningManagementSystem.BLL.DTOs;

public class GetUserDto
{
    public string DisplayName { get; set; } = string.Empty; 
    public string UserName { get; set; } = string.Empty; 
    public string Email { get; set; } = string.Empty; 
    public string PhoneNumber { get; set; } = string.Empty; 
    public string Role { get; set; } = string.Empty; 
    public int CoursesCreated { get; set; }
    public int CoursesEnrolled { get; set; }}
