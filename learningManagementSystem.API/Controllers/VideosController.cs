

using learningManagementSystem.API.Filter;

namespace learningManagementSystem.API.Controllers;


[Route("api/[controller]")]
[ApiController]
public class VideosController : ControllerBase
{
	private readonly IVideoService _videoService;
	private readonly ICacheHelper _cacheHelper;

	public VideosController(IVideoService videoService, ICacheHelper cacheHelper)
    {
		_videoService = videoService;
		_cacheHelper = cacheHelper;
	}

	[HttpGet("GetVideo/{id}")]
	[Authorize]
	[ServiceFilter(typeof(AccessVideosFilter))]
	public async Task<ActionResult> GetVideo(Guid id)
	{
		var cacheKey = "GetVideo";
		var video = await _cacheHelper.GetDataFromCache<GetVideoByIdWithIncludesDto>(cacheKey);
		if(video is not null)
		{
			return Ok(video);
		}

		video = await _videoService.GetByIdWithIncludes(id);
		if (video is null)
		{
			return BadRequest("video not found");
		}

		await _cacheHelper.SetDataInCache(cacheKey, video);

		return Ok(video);
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
