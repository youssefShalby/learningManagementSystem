

namespace learningManagementSystem.API.Controllers;



[Route("api/[controller]")]
[ApiController]
public class CommentReplyController : ControllerBase
{
	private readonly ICommentReplyService _commentReplyService;

	public CommentReplyController(ICommentReplyService commentReplyService)
    {
		_commentReplyService = commentReplyService;
	}

	[HttpPost]
	public async Task<ActionResult> CreateReply(CreateCommentReplyDto model)
	{
		var result = await _commentReplyService.CreateCommentReplyAsync(model);
		if (!result.IsSuccessed)
		{
			return BadRequest(result);
		}
		return StatusCode(201, result);
	}

	[HttpPut]
	public async Task<ActionResult> UpdateReply([FromHeader]Guid id, UpdateCommentReplyDto model)
	{
		var result = await _commentReplyService.UpdateCommentReplyAsync(id, model);
		if (!result.IsSuccessed)
		{
			return BadRequest(result);
		}
		return Ok(result);
	}

	[HttpDelete]
	public async Task<ActionResult> DeleteReply([FromHeader] Guid id)
	{
		var result = await _commentReplyService.DeleteCommentReplyAsync(id);
		if (!result.IsSuccessed)
		{
			return BadRequest(result);
		}
		return Ok(result);
	}
}
