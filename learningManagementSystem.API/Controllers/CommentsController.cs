
namespace learningManagementSystem.API.Controllers;


[Route("api/[controller]")]
[ApiController]
public class CommentsController : ControllerBase
{
	private readonly ICommentService _commentService;

	public CommentsController(ICommentService commentService)
    {
		_commentService = commentService;
	}

	[HttpGet("GetCommentReplies")]
	public async Task<ActionResult> GetCommentReplies([FromHeader]Guid id)
	{
		var replies = await _commentService.GetCommentRepliesAsync(id);
		if(replies is null)
		{
			return NotFound();
		}

		return Ok(replies);
	}

	[HttpPost("CreateCourseComment")]
	public async Task<ActionResult> CreateCourseComment(CreateCommentForCourseDto model)
	{
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
		var result = await _commentService.CreateCommentForVideoAsync(model);
		if (!result.IsSuccessed)
		{
			return BadRequest(result);
		}

		return StatusCode(201, result);
	}

	[HttpPut]
	public async Task<ActionResult> UpdateComment([FromHeader]Guid id, UpdateCommentDto model)
	{
		var result = await _commentService.UpdateCommentAsync(id, model);
		if(!result.IsSuccessed)
		{
			return BadRequest(result);
		}

		return Ok(result);
	}

	[HttpDelete]
	public async Task<ActionResult> DeleteComment([FromHeader] Guid id)
	{
		var result = await _commentService.DeleteCommentAsync(id);
		if (!result.IsSuccessed)
		{
			return BadRequest(result);
		}

		return Ok(result);
	}



}
