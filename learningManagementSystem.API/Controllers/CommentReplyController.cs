

namespace learningManagementSystem.API.Controllers;



[Route("api/[controller]")]
[ApiController]
[Authorize]
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
		var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
		model.UserId = userId ?? null!;
		var result = await _commentReplyService.CreateCommentReplyAsync(model);
		if (!result.IsSuccessed)
		{
			return BadRequest(result);
		}
		return StatusCode(201, result);
	}

	[HttpPut("{id}")]
	public async Task<ActionResult> UpdateReply([FromRoute]Guid id, UpdateCommentReplyDto model)
	{
		var result = await _commentReplyService.UpdateCommentReplyAsync(id, model);
		if (!result.IsSuccessed)
		{
			return BadRequest(result);
		}
		return Ok(result);
	}

	[HttpDelete("{id}")]
	public async Task<ActionResult> DeleteReply(Guid id)
	{
		var result = await _commentReplyService.DeleteCommentReplyAsync(id);
		if (!result.IsSuccessed)
		{
			return BadRequest(result);
		}
		return Ok(result);
	}
}
