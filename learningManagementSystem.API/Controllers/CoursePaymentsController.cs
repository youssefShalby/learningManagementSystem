

namespace learningManagementSystem.API.Controllers;



[Route("api/[controller]")]
[ApiController]
[Authorize]
public class CoursePaymentsController : ControllerBase
{
	private readonly ICoursePaymentService _coursePaymentService;
	private readonly ICacheHelper _cacheHelper;

	public CoursePaymentsController(ICoursePaymentService coursePaymentService, ICacheHelper cacheHelper)
    {
		_coursePaymentService = coursePaymentService;
		_cacheHelper = cacheHelper;
	}

	[HttpGet("GetCourse/{id}")]
	public async Task<ActionResult> GetById(Guid id)
	{
		var cacheKey = "GetCourseForPayment";
		var course = await _cacheHelper.GetDataFromCache<GetCoursePaymentDto>(cacheKey);
		course = await _coursePaymentService.GetByIdAsync(id);
		if(course is not null)
		{
			return Ok(course);
		}

		if(course is null)
		{
			return NotFound("not found");
		}

		await _cacheHelper.SetDataInCache(cacheKey, course);

		return Ok(course);
	}

	[HttpPost]
	public async Task<ActionResult> CreateCoursePayment(CreateCoursePaymentDto model)
	{
		var result = await _coursePaymentService.CreateCoursePaymentAsync(model);
		if (!result.IsSuccessed)
		{
			return BadRequest(result);
		}
		return StatusCode(201, result);
	}

	[HttpPut("{id}")]
	public async Task<ActionResult> UpdateCoursePayment([FromRoute] Guid id, UpdateCoursePaymentDto model)
	{
		var result = await _coursePaymentService.UpdateCoursePaymentAsync(id, model);
		if (!result.IsSuccessed)
		{
			return BadRequest(result);
		}
		return Ok(result);
	}

	[HttpDelete("{id}")]
	public async Task<ActionResult> DeleteCoursePayment(Guid id)
	{
		var result = await _coursePaymentService.DeleteCoursePaymentAsync(id);
		if (!result.IsSuccessed)
		{
			return BadRequest(result);
		}
		return Ok(result);
	}

}
