

namespace learningManagementSystem.API.Controllers;



[Route("api/[controller]")]
[ApiController]
public class CoursePaymentsController : ControllerBase
{
	private readonly ICoursePaymentService _coursePaymentService;

	public CoursePaymentsController(ICoursePaymentService coursePaymentService)
    {
		_coursePaymentService = coursePaymentService;
	}

	[HttpGet("GetCourse")]
	public async Task<ActionResult> GetById([FromHeader]Guid id)
	{
		var result = await _coursePaymentService.GetByIdAsync(id);
		if(result is null)
		{
			return NotFound("not found");
		}

		return Ok(result);
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

	[HttpPut]
	public async Task<ActionResult> UpdateCoursePayment([FromHeader] Guid id, UpdateCoursePaymentDto model)
	{
		var result = await _coursePaymentService.UpdateCoursePaymentAsync(id, model);
		if (!result.IsSuccessed)
		{
			return BadRequest(result);
		}
		return Ok(result);
	}

	[HttpDelete]
	public async Task<ActionResult> DeleteCoursePayment([FromHeader] Guid id)
	{
		var result = await _coursePaymentService.DeleteCoursePaymentAsync(id);
		if (!result.IsSuccessed)
		{
			return BadRequest(result);
		}
		return Ok(result);
	}

}
