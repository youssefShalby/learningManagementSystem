


namespace learningManagementSystem.BLL.Service;

public class CommentReplyService : ICommentReplyService
{
	private readonly IUnitOfWork _unitOfWork;

	public CommentReplyService(IUnitOfWork unitOfWork)
    {
		_unitOfWork = unitOfWork;
	}

	public async Task<CommonResponse> CreateCommentReplyAsync(CreateCommentReplyDto model)
	{
		CommentReply newCommentReply = new CommentReply
		{
			CreatedAt = DateTime.Now,
			Id = Guid.NewGuid(),
			UserId = model.UserId,
			CommentId = model.CommentId,
			Content = model.Content,
		};

		try
		{
			await _unitOfWork.CommentRelyRepo.CreateAsync(newCommentReply);
			await _unitOfWork.CommentRelyRepo.SaveChangesAsync();
			return new CommonResponse("reply created..!!", true);
		}

		catch (Exception ex)
		{
			return new CommonResponse($"cannot create relpy because {ex.Message}", false);
		}
	}

	public async Task<CommonResponse> DeleteCommentReplyAsync(Guid id)
	{
		var replyToDelete = await _unitOfWork.CommentRelyRepo.GetByIdAsync(id);
		if(replyToDelete is null)
		{
			return new CommonResponse("reply not found..!!", false);
		}

		try
		{
			_unitOfWork.CommentRelyRepo.Delete(replyToDelete);
			_unitOfWork.CommentRelyRepo.SaveChanges();
			return new CommonResponse("reply deleted..!!", true);
		}

		catch (Exception ex)
		{
			return new CommonResponse($"cannot delete relpy because {ex.Message}", false);
		}

	}

	public async Task<CommonResponse> UpdateCommentReplyAsync(Guid id, UpdateCommentReplyDto model)
	{
		var replyToUpdate = await _unitOfWork.CommentRelyRepo.GetByIdAsync(id);
		if (replyToUpdate is null)
		{
			return new CommonResponse("reply not found..!!", false);
		}

		try
		{
			replyToUpdate.Content = model.Content;
			_unitOfWork.CommentRelyRepo.Update(replyToUpdate);
			_unitOfWork.CommentRelyRepo.SaveChanges();
			return new CommonResponse("reply updated..!!", true);
		}

		catch (Exception ex)
		{
			return new CommonResponse($"cannot update relpy because {ex.Message}", false);
		}
	}
}
