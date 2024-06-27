

namespace learningManagementSystem.API.Controllers;


[Route("api/[controller]")]
[ApiController]
public class LessonsController : ControllerBase
{
	private readonly ILessonService _lessonService;

	public LessonsController(ILessonService lessonService)
    {
		_lessonService = lessonService;
	}

	[HttpGet("LessonVideos/{id}")]
	public async Task<ActionResult> GetVideos(Guid id)
	{
		var vidoes = await _lessonService.GetVideosOfLessonAsync(id);
		if(vidoes is null)
		{
			return NotFound();
		}
		return Ok(vidoes);
	}

	[HttpPost]
	[Authorize(policy: "Instructor")]
	public async Task<ActionResult> CreateLesson(CreateLessonDto model)
	{
		var result = await _lessonService.CreateLessonAsync(model);
		if (!result.IsSuccessed)
		{
			return BadRequest(result);
		}

		return StatusCode(201, result);
	}

	[HttpPut("{id}")]
	[Authorize(policy: "Instructor")]
	public async Task<ActionResult> UpdateLesson([FromRoute]Guid id, UpdateLessonDto model)
	{
		var result = await _lessonService.UpdateLessonAsync(id, model);
		if (!result.IsSuccessed)
		{
			return BadRequest(result);
		}

		return Ok(result);
	}
	
	[HttpDelete("{id}")]
	[Authorize(policy: "Instructor")]
	public async Task<ActionResult> DeleteLesson(Guid id)
	{
		var result = await _lessonService.DeleteLessonAsync(id);
		if (!result.IsSuccessed)
		{
			return BadRequest(result);
		}

		return Ok(result);
	}

	[HttpDelete("MarkLessonAsDeleted/{id}")]
	[Authorize(policy: "Instructor")]
	public async Task<ActionResult> MarkLessonAsDeleted(Guid id)
	{
		var result = await _lessonService.MarkLessonAsDeletedAsync(id);
		if (!result.IsSuccessed)
		{
			return BadRequest(result);
		}

		return Ok(result);
	}
}
