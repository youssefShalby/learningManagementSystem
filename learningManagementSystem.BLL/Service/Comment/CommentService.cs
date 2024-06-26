


namespace learningManagementSystem.BLL.Service;

public class CommentService : ICommentService
{
	private readonly IUnitOfWork _unitOfWork;

	public CommentService(IUnitOfWork unitOfWork)
    {
		_unitOfWork = unitOfWork;
	}

	public async Task<CommonResponse> CreateCommentForCourseAsync(CreateCommentForCourseDto model)
	{
		Comment newComment = new Comment
		{
			Content = model.Content,
			CreatedAt = DateTime.Now,
			CourseId = model.CourseId,
			UserId = model.UserId,
			Id = Guid.NewGuid()
		};

		try
		{
			await _unitOfWork.CommentRepo.CreateAsync(newComment);
			await _unitOfWork.CommentRepo.SaveChangesAsync();
			return new CommonResponse("comment created..!!", true);
		}
		catch(Exception ex)
		{
			return new CommonResponse($"cannot create comment because {ex.Message}", false);
		}
	}

	public async Task<CommonResponse> CreateCommentForVideoAsync(CreateCommentForVideoDto model)
	{
		Comment newComment = new Comment
		{
			Content = model.Content,
			CreatedAt = DateTime.Now,
			UserId = model.UserId,
			VideoId = model.VideoId,
			Id = Guid.NewGuid()
		};

		try
		{
			await _unitOfWork.CommentRepo.CreateAsync(newComment);
			await _unitOfWork.CommentRepo.SaveChangesAsync();
			return new CommonResponse("comment created..!!", true);
		}
		catch (Exception ex)
		{
			return new CommonResponse($"cannot create comment because {ex.Message}", false);
		}
	}

	public async Task<CommonResponse> DeleteCommentAsync(Guid id)
	{
		var commentToDelete = await _unitOfWork.CommentRepo.GetByIdAsync(id);
		if (commentToDelete is null)
		{
			return new CommonResponse("comment not found ..!!", false);
		}

		try
		{	
			_unitOfWork.CommentRepo.Delete(commentToDelete);
			_unitOfWork.CommentRepo.SaveChanges();
			return new CommonResponse("comment deleted..!!", true);
		}
		catch (Exception ex)
		{
			return new CommonResponse($"cannot delete comment because {ex.Message}", false);
		}
	}

	public async Task<IEnumerable<GetRepliesDto>> GetCommentRepliesAsync(Guid id)
	{
		var comment = await _unitOfWork.CommentRepo.GetCommentWithRpliesAsync(id);
		if(comment is null)
		{
			return null!;
		}

		if(comment.Replies is null)
		{
			return null!;
		}

		return comment.Replies?.Select(reply => new GetRepliesDto
		{
			Content = reply.Content,
			CreatedAt = reply.CreatedAt,
			UserId = reply.UserId,
			UserName = reply.AppUser?.UserName ?? "NA"

		}).ToList()!;
	}

	public async Task<CommonResponse> UpdateCommentAsync(Guid id, UpdateCommentDto model)
	{
		var commentToUpdate = await _unitOfWork.CommentRepo.GetByIdAsync(id);
		if(commentToUpdate is null)
		{
			return new CommonResponse("comment not found ..!!", false);
		}

		try
		{
			commentToUpdate.Content = model.Content;
			_unitOfWork.CommentRepo.Update(commentToUpdate);
			_unitOfWork.CommentRepo.SaveChanges();
			return new CommonResponse("comment updated..!!", true);
		}
		catch (Exception ex)
		{
			return new CommonResponse($"cannot update comment because {ex.Message}", false);
		}
	}
}
