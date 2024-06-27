

namespace learningManagementSystem.API.Controllers;


[Route("api/[controller]")]
[ApiController]
[Authorize(policy: "Admin")]
public class AdminDashboardController : ControllerBase
{
	private readonly IUserService _userService;
	private readonly IRoleService _roleService;
	private readonly IOrderService _orderService;
	private readonly ICourseService _courseService;
	private readonly ICategoryService _categoryService;

	public AdminDashboardController(IUserService userService, IRoleService roleService, 
			IOrderService orderService, ICourseService courseService, ICategoryService categoryService)
    {
		_userService = userService;
		_roleService = roleService;
		_courseService = courseService;
		_categoryService = categoryService;
		_orderService = orderService;
	}

	[HttpPost("AssignRoleAdmintIntoUser")]
	public async Task<ActionResult> MarkUserAsAdmin([FromHeader]string email)
	{
		var result = await _userService.MarkUserAsAdminAsync(email);
		if (!result.IsSuccessed)
		{
			return BadRequest(result);
		}
		return Ok(result);
	}

	[HttpPost("AddRole")]
	public async Task<ActionResult> CreateRole(AddRoleDto model)
	{
		var result = await _roleService.CreateRoleAsync(model);
		if (result.IsSuccessed)
		{
			return StatusCode(201, result); //> created
		}
		return BadRequest(result);
	}

	[HttpDelete("RemoveRole")]
	public async Task<ActionResult> CreateRole([FromHeader] string name)
	{
		var result = await _roleService.RemoveRoleAsync(name);
		if (result.IsSuccessed)
		{
			return StatusCode(200, result); //> Ok, Deleted
		}
		return BadRequest(result);
	}

	[HttpPost("GetUser")]
	public async Task<ActionResult> GetUser([FromHeader]string userNameOrEmail)
	{
		var result = await _userService.GetUserByEmailOrUserNameAsync(userNameOrEmail);
		if (result is null)
		{
			return NotFound(); 
		}
		return Ok(result);
	}

	[HttpGet("GetOrdersForLastMonth")]
	public async Task<ActionResult> GetOrdersForLastMonth([FromHeader] int pageNumber)
	{
		var result = await _orderService.GetOrdersOfLastMonthAsync(pageNumber);
		if (result is null)
		{
			return NotFound("there are no orders for last month..!!");
		}
		return Ok(result);
	}

	[HttpGet("GetOrdersForLastYear")]
	public async Task<ActionResult> GetOrdersForLastYear([FromHeader] int pageNumber)
	{
		var result = await _orderService.GetOrdersOfLastYearAsync(pageNumber);
		if (result is null)
		{
			return NotFound("there are no orders for last year..!!");
		}
		return Ok(result);
	}

	[HttpGet("GetCoursesForLastMonth")]
	public async Task<ActionResult> GetCoursesForLastMonth([FromHeader] int pageNumber)
	{
		var result = await _courseService.GetCoursesOfLastMonthAsync(pageNumber);
		if (result is null)
		{
			return NotFound("there are no courses for last month..!!");
		}
		return Ok(result);
	}

	[HttpGet("GetCoursesForLastYear")]
	public async Task<ActionResult> GetCoursesForLastYear([FromHeader] int pageNumber)
	{
		var result = await _courseService.GetCoursesOfLastYearAsync(pageNumber);
		if (result is null)
		{
			return NotFound("there are no courses for last year..!!");
		}
		return Ok(result);
	}

	[HttpGet("GetAppInfo")]
	public async Task<ActionResult<GetAppInfoDto>> GetAppInfo()
	{
		var blockedUserCount = await _userService.GetBockedUsersCountAsync();
		var UnblockedUserCount = _userService.GetUsersNumber();
		return new GetAppInfoDto
		{
			CorsesCountOfLastMonth = _courseService.GetCoursesCountOfLastMonth(),
			CorsesCountOfLastYear = _courseService.GetCoursesCountOfLastYear(),
			OrdersCountOfLastMonth = _orderService.GetOrdersCountOfLastMonth(),
			OrdersCountOfLastYear = _orderService.GetOrdersCountOfLastYear(),
			AllCategoriesCount = _categoryService.GetCategoriesCount(),
			AllCoursesCount = _courseService.GetCoursesCount(),
			AllOrdersCount = _orderService.GetOrdersCount(),
			AppUsersCount = _userService.GetUsersNumber(),
			BlockedUserCount = await _userService.GetBockedUsersCountAsync(),
			UnBlockedUserCount = Math.Abs(UnblockedUserCount - blockedUserCount),
			InstructorsCount = await _userService.GetInstructorsNumberAsync(),
			StudentCount = await _userService.GetStudentsNumberAsync(),
			AdminsCount = await _userService.GetAdminsCountAsync(),
		};
	}

	[HttpPost("BlockUser")]
	public async Task<ActionResult> BlcokUser(BlockUserDto model)
	{
		var result = await _userService.BlockUserForPeriodAsync(model);
		if (!result.IsSuccessed)
		{
			return BadRequest(result);
		}
		return Ok(result);	
	}

	[HttpPost("UnBlockUser")]
	public async Task<ActionResult> BlcokUser([FromHeader]string email)
	{
		var result = await _userService.UnBlockUserForPeriodAsync(email);
		if (!result.IsSuccessed)
		{
			return BadRequest(result);
		}
		return Ok(result);
	}
}
