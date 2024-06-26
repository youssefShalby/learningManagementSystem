


namespace learningManagementSystem.BLL.Service;

public class LessonService : ILessonService
{
	private readonly IUnitOfWork _unitOfWork;

	public LessonService(IUnitOfWork unitOfWork)
    {
		_unitOfWork = unitOfWork;
	}

	public async Task<CommonResponse> CreateLessonAsync(CreateLessonDto model)
	{
		try
		{
			Lesson newLesson = new Lesson()
			{
				Name = model.Name,
				CourseId = model.CourseId,
			};

			await _unitOfWork.LessonRepo.CreateAsync(newLesson);
			await _unitOfWork.LessonRepo.SaveChangesAsync();
			return new CommonResponse("lesson created..!!", true);
		}
		catch(Exception ex)
		{
			return new CommonResponse($"cannot create lesson rigth now because {ex.Message}", false);
		}
	}

	public async Task<CommonResponse> DeleteLessonAsync(Guid id)
	{
		var LessonToDelete = await _unitOfWork.LessonRepo.GetByIdAsync(id);
		if (LessonToDelete == null)
		{
			return new CommonResponse("lesson not founded..!!", false);
		}

		try
		{	_unitOfWork.LessonRepo.Delete(LessonToDelete);
			_unitOfWork.LessonRepo.SaveChanges();
			return new CommonResponse("lesson Deleteed..!!", true);
		}
		catch (Exception ex)
		{
			return new CommonResponse($"cannot Delete lesson rigth now because {ex.Message}", false);
		}
	}

	public async Task<IEnumerable<GetVideoForLessonDto>> GetVideosOfLessonAsync(Guid id)
	{
		var LessonToDelete = await _unitOfWork.LessonRepo.GetByIdWithIncludesAsync(id);
		if (LessonToDelete == null || LessonToDelete.IsDeleted)
		{
			return null!;
		}

		return LessonToDelete.Videos?.Select(vidoe => VideoMapper.ToGetForLessonDto(vidoe)).ToList()!;
	}

	public async Task<CommonResponse> MarkLessonAsDeletedAsync(Guid id)
	{
		var LessonToDelete = await _unitOfWork.LessonRepo.GetByIdAsync(id);
		if (LessonToDelete == null)
		{
			return new CommonResponse("lesson not founded..!!", false);
		}

		try
		{
			LessonToDelete.IsDeleted = true;
			_unitOfWork.LessonRepo.Update(LessonToDelete);
			_unitOfWork.LessonRepo.SaveChanges();
			return new CommonResponse("lesson Deleted..!!", true);
		}
		catch (Exception ex)
		{
			return new CommonResponse($"cannot Delete lesson rigth now because {ex.Message}", false);
		}
	}

	public async Task<CommonResponse> UpdateLessonAsync(Guid id, UpdateLessonDto model)
	{
		var LessonToUpdate = await _unitOfWork.LessonRepo.GetByIdAsync(id);
		if(LessonToUpdate == null)
		{
			return new CommonResponse("lesson not founded..!!", false);
		}

		try
		{
			LessonToUpdate.Name = model.Name;
			_unitOfWork.LessonRepo.Update(LessonToUpdate);
			_unitOfWork.LessonRepo.SaveChanges();
			return new CommonResponse("lesson updated..!!", true);
		}
		catch (Exception ex)
		{
			return new CommonResponse($"cannot update lesson rigth now because {ex.Message}", false);
		}
	}
}
