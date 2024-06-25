


namespace learningManagementSystem.BLL.Service;

public interface IRoleService
{
	Task<CommonResponse> CreateRoleAsync(AddRoleDto model);
	Task<CommonResponse> RemoveRoleAsync(string name);
}
