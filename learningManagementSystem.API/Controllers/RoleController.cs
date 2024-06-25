

namespace learningManagementSystem.API.Controllers;


[Route("api/[controller]")]
[ApiController]
public class RoleController : ControllerBase
{
	private readonly IRoleService _roleService;

	public RoleController(IRoleService roleService)
    {
		_roleService = roleService;
	}

	[HttpPost]
	public async Task<ActionResult> CreateRole(AddRoleDto model)
	{
		var result = await _roleService.CreateRoleAsync(model);
		if (result.IsSuccessed)
		{
			return StatusCode(201, result); //> created
		}
		return BadRequest(result);
	}

	[HttpDelete]
	public async Task<ActionResult> CreateRole([FromHeader] string name)
	{
		var result = await _roleService.RemoveRoleAsync(name);
		if (result.IsSuccessed)
		{
			return StatusCode(200, result); //> Ok, Deleted
		}
		return BadRequest(result);
	}
}
