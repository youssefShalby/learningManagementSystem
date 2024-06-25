

namespace learningManagementSystem.BLL.Service;

public class RoleService : IRoleService
{
	private readonly IUnitOfWork _unitOfWork;
	public RoleService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<CommonResponse> CreateRoleAsync(AddRoleDto model)
	{
		IdentityRole newRole = new()
		{
			Name = model.Name,
		};

		var result = await _unitOfWork.RoleManager.CreateAsync(newRole);
		if(!result.Succeeded)
		{
			return new CommonResponse("cannot create role right now..!!", false);
		}
		return new CommonResponse("role creating success..!!", true);
	}

	public async Task<CommonResponse> RemoveRoleAsync(string name)
	{
		if(string.IsNullOrEmpty(name))
		{
			return new CommonResponse("the role name is not valid..!!", false);
		}

		var role = await _unitOfWork.RoleManager.FindByNameAsync(name);
		if(role is null)
		{
			return new CommonResponse("the role not founded..!!", false);
		}

		var result = await _unitOfWork.RoleManager.DeleteAsync(role);
		if (!result.Succeeded)
		{
			return new CommonResponse("cannot delete role right now..!!", false);
		}
		return new CommonResponse("role deleted..!!", true);
	}
}
