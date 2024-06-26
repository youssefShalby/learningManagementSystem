

namespace learningManagementSystem.API.Controllers;



[Route("api/[controller]")]
[ApiController]
public class CoursesController : ControllerBase
{
	private readonly ICourseService _courseService;
	private readonly ICourseRepo cs;

	public CoursesController(ICourseService courseService, ICourseRepo cc)
    {
		_courseService = courseService;
		cs = cc;
	}

	[HttpGet("UnlockVideos")]
	public async Task<ActionResult> UnlockVideos([FromHeader]Guid id)
	{
		var result = await _courseService.UnlockVideos(id);
		return Ok(result);
	}

	[HttpGet("GetAllToShow/{pageNumber}")]
	public async Task<ActionResult> GetAllToShow(int pageNumber)
	{
		var result = await _courseService.GetAllToShowAsync(pageNumber);
		if(result is null)
		{
			return BadRequest(result);
		}
		return Ok(result);
	}	
	
	
	[HttpPost("GetAll")]
	public async Task<ActionResult> GetAll(CourseQueryHandler query)
	{
		var result = await _courseService.GetAllWithQueryAsync(query);
		if(result is null)
		{
			return BadRequest(result);
		}
		return Ok(result);
	}

	[HttpGet("GetCourse")]
	public async Task<ActionResult> GetCourseWithIncludes(Guid id)
	{
		var result = await _courseService.GetByIdWithIncludesAsync(id);
		if (result is null)
		{
			return BadRequest(result);
		}
		return Ok(result);
	}

	[HttpPost("StudentCourses")]
	public async Task<ActionResult> StudentCourses(CourseQueryHandler query, [FromHeader]string userId)
	{
		var result = await _courseService.GetStudentCoursesAsync(query, userId);
		if(result is null)
		{
			return BadRequest(result);
		}
		return Ok(result);
	}


	[HttpPost("InstructorCourses")]
	public async Task<ActionResult> InstructorCourses(CourseQueryHandler query, [FromHeader]string userId)
	{
		var result = await _courseService.GetInstructorCoursesAsync(query, userId);
		if(result is null)
		{
			return BadRequest(result);
		}
		return Ok(result);
	}

	[HttpPost]
	public async Task<ActionResult> Create(CreateCourseDto model)
	{
		var result = await _courseService.CreateCourseAsync(model);
		if (!result.IsSuccessed)
		{
			return BadRequest(result);
		}
		return StatusCode(201, result);
	}

	[HttpPut]
	public async Task<ActionResult> Update([FromHeader]Guid id, UpdateCourseDto model)
	{
		var result = await _courseService.UpdateCourseAsync(id, model);
		if (!result.IsSuccessed)
		{
			return BadRequest(result);
		}
		return StatusCode(201, result);
	}
	
	[HttpDelete]
	public async Task<ActionResult> Delete([FromHeader]Guid id)
	{
		var result = await _courseService.DeleteCourseAsync(id);
		if (!result.IsSuccessed)
		{
			return BadRequest(result);
		}
		return StatusCode(201, result);
	}

	[HttpDelete("MarkCourseAsDeleted")]
	public async Task<ActionResult> MarkAsDeleted([FromHeader] Guid id)
	{
		var result = await _courseService.MarkCourseAsDeletedAsync(id);
		if (!result.IsSuccessed)
		{
			return BadRequest(result);
		}
		return StatusCode(201, result);
	}
}
