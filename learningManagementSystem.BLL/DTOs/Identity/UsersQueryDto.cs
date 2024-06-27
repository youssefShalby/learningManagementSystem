

namespace learningManagementSystem.BLL.DTOs;

public class UsersQueryDto
{
	public int PageNumber { get; set; }
	public int PageSize { get; set; } = 10;
	public bool IsDescending { get; set; }
	public string SortBy { get; set; } = string.Empty;
	public string Email { get; set; } = string.Empty;
	public string UserName { get; set; } = string.Empty;
}
