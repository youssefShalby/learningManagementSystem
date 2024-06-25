

namespace learningManagementSystem.BLL.Service;

public interface IHandlerService
{
	Task<CommonResponse> RegisterHandlerAsync(RegisterDto model, string mainRole, params string[] otherRoles);
}
