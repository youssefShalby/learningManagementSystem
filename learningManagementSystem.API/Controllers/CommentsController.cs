
namespace learningManagementSystem.API.Controllers;


[Route("api/[controller]")]
[ApiController]
[Authorize]
public class CommentsController : ControllerBase
{
	private readonly ICommentService _commentService;
	private readonly ICacheHelper _cacheHelper;

	public CommentsController(ICommentService commentService, ICacheHelper cacheHelper)
    {
		_commentService = commentService;
		_cacheHelper = cacheHelper;
	}

	[HttpGet("GetCommentReplies/{id}")]
	[AllowAnonymous]
	public async Task<ActionResult> GetCommentReplies(Guid id)
	{
		var cacheKey = "GetCommentReplies";
		var replies = await _cacheHelper.GetDataFromCache<IEnumerable<GetRepliesDto>>(cacheKey);
		if(replies is not null)
		{
			return Ok(replies);
		}

		replies = await _commentService.GetCommentRepliesAsync(id);
		if(replies is null)
		{
			return NotFound();
		}

		await _cacheHelper.SetDataInCache(cacheKey, replies);

		return Ok(replies);
	}

	[HttpPost("CreateCourseComment")]
	public async Task<ActionResult> CreateCourseComment(CreateCommentForCourseDto model)
	{
		var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
		model.UserId = userId ?? null!;

		var result = await _commentService.CreateCommentForCourseAsync(model);
		if (!result.IsSuccessed)
		{
			return BadRequest(result);
		}

		return StatusCode(201, result);
	}

	[HttpPost("CreateVideoComment")]
	public async Task<ActionResult> CreateVideoComment(CreateCommentForVideoDto model)
	{
		var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
		model.UserId = userId ?? null!;

		var result = await _commentService.CreateCommentForVideoAsync(model);
		if (!result.IsSuccessed)
		{
			return BadRequest(result);
		}

		return StatusCode(201, result);
	}

	[HttpPut("{id}")]
	public async Task<ActionResult> UpdateComment([FromRoute]Guid id, UpdateCommentDto model)
	{
		var result = await _commentService.UpdateCommentAsync(id, model);
		if(!result.IsSuccessed)
		{
			return BadRequest(result);
		}

		return Ok(result);
	}

	[HttpDelete("{id}")]
	public async Task<ActionResult> DeleteComment(Guid id)
	{
		var result = await _commentService.DeleteCommentAsync(id);
		if (!result.IsSuccessed)
		{
			return BadRequest(result);
		}

		return Ok(result);
	}



}
