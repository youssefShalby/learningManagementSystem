

namespace learningManagementSystem.API.Controllers;


[Route("api/[controller]")]
[ApiController]
public class LessonsController : ControllerBase
{
	private readonly ILessonService _lessonService;
	private readonly ICacheHelper _cacheHelper;

	public LessonsController(ILessonService lessonService, ICacheHelper cacheHelper)
    {
		_lessonService = lessonService;
		_cacheHelper = cacheHelper;
	}

	[HttpGet("LessonVideos/{id}")]
	public async Task<ActionResult> GetVideos(Guid id)
	{
		var cacheKey = "GetLessonVideos";
		var videos = await _cacheHelper.GetDataFromCache<IEnumerable<GetVideoForLessonDto>>(cacheKey);
		if(videos is not null)
		{
			return Ok(videos);
		}

		videos = await _lessonService.GetVideosOfLessonAsync(id);
		if(videos is null)
		{
			return NotFound();
		}

		await _cacheHelper.SetDataInCache(cacheKey, videos);

		return Ok(videos);
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
