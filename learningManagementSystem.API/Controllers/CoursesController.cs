

namespace learningManagementSystem.API.Controllers;



[Route("api/[controller]")]
[ApiController]
public class CoursesController : ControllerBase
{
	private readonly ICourseService _courseService;

	public CoursesController(ICourseService courseService)
	{
		_courseService = courseService;
	}

	[HttpGet("UnlockVideos/{LessonId}")]
	[Authorize(policy: "Instructor")]
	public async Task<ActionResult> UnlockVideos(Guid LessonId)
	{
		var result = await _courseService.UnlockVideos(LessonId);
		return Ok(result);
	}

	[HttpGet("GetAllToShow/{pageNumber}")]
	[Authorize]
	public async Task<ActionResult> GetAllToShow(int pageNumber)
	{
		var result = await _courseService.GetAllToShowAsync(pageNumber);
		if (result is null)
		{
			return BadRequest(result);
		}
		return Ok(result);
	}


	[HttpPost("GetAll")]
	[Authorize]
	public async Task<ActionResult> GetAll(CourseQueryHandler query)
	{
		var result = await _courseService.GetAllWithQueryAsync(query);
		if (result is null)
		{
			return BadRequest(result);
		}
		return Ok(result);
	}

	[HttpGet("GetCourse/{id}")]
	[Authorize]
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
	[Authorize(policy: "Student")]
	public async Task<ActionResult> StudentCourses(CourseQueryHandler query)
	{
		var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

		var result = await _courseService.GetStudentCoursesAsync(query, userId ?? null!);
		if (result is null)
		{
			return BadRequest(result);
		}
		return Ok(result);
	}


	[HttpPost("InstructorCourses")]
	[Authorize(policy: "Instructor")]
	public async Task<ActionResult> InstructorCourses(CourseQueryHandler query)
	{
		var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

		var result = await _courseService.GetInstructorCoursesAsync(query, userId ?? null!);
		if (result is null)
		{
			return BadRequest(result);
		}
		return Ok(result);
	}

	[HttpPost("EnrollStudent")]
	[Authorize(policy: "Instructor")]
	public async Task<ActionResult> EnrollStudent(EnrollStudentToCourseDto model)
	{
		var result = await _courseService.EnrollStudentToCourseAsync(model);
		if (!result.IsSuccessed)
		{
			return BadRequest(result);
		}
		return Ok(result);
	}


	[HttpPost("OutStudent")]
	[Authorize(policy: "Instructor")]
	public async Task<ActionResult> OutStudent(OutUserFromCourseDto model)
	{
		var result = await _courseService.OutStudentFromCourseAsync(model);
		if (!result.IsSuccessed)
		{
			return BadRequest(result);
		}
		return Ok(result);
	}

	[HttpPost]
	[Authorize(policy: "Instructor")]
	public async Task<ActionResult> Create(CreateCourseDto model)
	{
		var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

		var result = await _courseService.CreateCourseAsync(model, userId ?? "");
		if (!result.IsSuccessed)
		{
			return BadRequest(result);
		}
		return StatusCode(201, result);
	}

	[HttpPut("{id}")]
	[Authorize(policy: "Instructor")]
	public async Task<ActionResult> Update([FromRoute] Guid id, UpdateCourseDto model)
	{
		var result = await _courseService.UpdateCourseAsync(id, model);
		if (!result.IsSuccessed)
		{
			return BadRequest(result);
		}
		return StatusCode(201, result);
	}

	[HttpDelete("{id}")]
	[Authorize(policy: "Instructor")]
	public async Task<ActionResult> Delete(Guid id)
	{
		var result = await _courseService.DeleteCourseAsync(id);
		if (!result.IsSuccessed)
		{
			return BadRequest(result);
		}
		return StatusCode(201, result);
	}

	[HttpDelete("MarkCourseAsDeleted/{id}")]
	[Authorize(policy: "Instructor")]
	public async Task<ActionResult> MarkAsDeleted(Guid id)
	{
		var result = await _courseService.MarkCourseAsDeletedAsync(id);
		if (!result.IsSuccessed)
		{
			return BadRequest(result);
		}
		return StatusCode(201, result);
	}
}
