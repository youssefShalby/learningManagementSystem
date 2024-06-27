

using learningManagementSystem.API.Filter;

namespace learningManagementSystem.API.Controllers;


[Route("api/[controller]")]
[ApiController]
public class VideosController : ControllerBase
{
	private readonly IVideoService _videoService;

	public VideosController(IVideoService videoService)
    {
		_videoService = videoService;
	}

	[HttpGet("GetVideo/{id}")]
	[Authorize]
	[ServiceFilter(typeof(AccessVideosFilter))]
	public async Task<ActionResult> GetVideo(Guid id)
	{
		var result = await _videoService.GetByIdWithIncludes(id);
		if (result is null)
		{
			return BadRequest("video not found");
		}

		return Ok(result);
	}

	[HttpGet("LockAndUnlockVideo/{id}")]
	[Authorize(policy: "Instructor")]
	public async Task<ActionResult> UnlockOrLock(Guid id)
	{
		var result = await _videoService.LockOrUnlockAsync(id);
		if (result is null)
		{
			return BadRequest("video not found");
		}

		return Ok(result);
	}

	[HttpPost]
	[Authorize(policy: "Instructor")]
	public async Task<ActionResult> UploadVideos(IEnumerable<UploadVideoDto> videos)
	{
		var result = await _videoService.UploadVideosAsync(videos);
		if (!result.IsSuccessed)
		{
			return BadRequest(result);
		}

		return StatusCode(201, result);
	}

	[HttpPut("{id}")]
	[Authorize(policy: "Instructor")]
	public async Task<ActionResult> UpdateVideo([FromRoute]Guid id, UpdateVideoDto model)
	{
		var result = await _videoService.UpdateVideoAsync(id, model);
		if (!result.IsSuccessed)
		{
			return BadRequest(result);
		}

		return Ok(result);
	}

	[HttpDelete("id")]
	[Authorize(policy: "Instructor")]
	public async Task<ActionResult> DeleteVideo(Guid id)
	{
		var result = await _videoService.DeleteVideoAsync(id);
		if (!result.IsSuccessed)
		{
			return BadRequest(result);
		}

		return Ok(result);
	}
}
