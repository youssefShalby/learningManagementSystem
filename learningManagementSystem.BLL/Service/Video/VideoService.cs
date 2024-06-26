


namespace learningManagementSystem.BLL.Service;

public class VideoService : IVideoService
{
	private readonly IUnitOfWork _unitOfWork;

	public VideoService(IUnitOfWork unitOfWork)
    {
		_unitOfWork = unitOfWork;
	}

	public async Task<CommonResponse> DeleteVideoAsync(Guid id)
	{
		var videoToDelete = await _unitOfWork.VideoRepo.GetByIdWithIncludesAsync(id);
		if (videoToDelete == null)
		{
			return new CommonResponse("video not found..!!", false);
		}

		try
		{
			//> delete comments of video first
			if(videoToDelete.Comments is not null)
			{
				_unitOfWork.CommentRepo.DeleteRange(videoToDelete.Comments);
			}
			
			_unitOfWork.VideoRepo.Update(videoToDelete);
			_unitOfWork.VideoRepo.SaveChanges();
			return new CommonResponse("video deleted..!!", true);
		}
		catch (Exception ex)
		{
			return new CommonResponse($"cannot delete video right now becuase {ex.Message}", false);
		}
	}

	public async Task<GetVideoByIdWithIncludesDto> GetByIdWithIncludes(Guid id)
	{
		var video = await _unitOfWork.VideoRepo.GetByIdWithIncludesAsync(id);
		if(video is null)
		{
			return null!;
		}

		return VideoMapper.ToGetWithIncludes(video);
	}

	public async Task<CommonResponse> UpdateVideoAsync(Guid id, UpdateVideoDto model)
	{
		var videoToUpdate = await _unitOfWork.VideoRepo.GetByIdWithIncludesAsync(id);
		if(videoToUpdate == null)
		{
			return new CommonResponse("video not found..!!", false);
		}

		try
		{
			videoToUpdate.Title = model.Title;
			videoToUpdate.Description = model.Description;
			videoToUpdate.IsLocked = model.IsLocked;

			_unitOfWork.VideoRepo.Update(videoToUpdate);
			_unitOfWork.VideoRepo.SaveChanges();
			return new CommonResponse("video updated..!!", true); 
		}
		catch(Exception ex)
		{
			return new CommonResponse($"cannot update video right now becuase {ex.Message}", false);
		}
	}

	public async Task<CommonResponse> UploadVideosAsync(IEnumerable<UploadVideoDto> videos)
	{
		try
		{
			var newVideos = videos.Select(video => VideoMapper.ToVideoModel(video)).ToList();
			await _unitOfWork.VideoRepo.CreateRangeAsync(newVideos);
			await _unitOfWork.VideoRepo.SaveChangesAsync();
			return new CommonResponse("video uploaded..!!", true);
		}
		catch (Exception ex)
		{
			return new CommonResponse($"cannot upload videos because {ex.Message}", false);
		}
	}
}
