




namespace learningManagementSystem.BLL.Service;

public class CourseService : ICourseService
{
	private readonly IUnitOfWork _unitOfWork;

	public CourseService(IUnitOfWork unitOfWork)
    {
		_unitOfWork = unitOfWork;
	}

	public async Task<CommonResponse> CreateCourseAsync(CreateCourseDto model, string userId)
	{
		try
		{
			var instructor = await _unitOfWork.InstructorRepo.GetByRefIdAsync(userId);

			model.InstructorId = instructor.Id;
			var newCourse = CourseMapper.FromCreateDto(model);
			await _unitOfWork.CourseRepo.CreateAsync(newCourse);
			await _unitOfWork.CourseRepo.SaveChangesAsync();
			return new CommonResponse("course created..!!", true);
		}
		catch (Exception ex)
		{
			return new CommonResponse($"cannot create course right now beause: {ex.Message}", false);
		}

	}

	public async Task<CommonResponse> UnlockVideos(Guid id)
	{
		await _unitOfWork.CourseRepo.UnlockCourseVideosAsync(id);
		return new CommonResponse("course videos unlocked..!", true);
	}

	public async Task<CommonResponse> DeleteCourseAsync(Guid id)
	{
		var courseToDelete = await _unitOfWork.CourseRepo.GetByIdAsync(id);
		if (courseToDelete is null)
		{
			return new CommonResponse("course not found..!!", false);
		}

		try
		{
			_unitOfWork.CourseRepo.Delete(courseToDelete);
			_unitOfWork.CourseRepo.SaveChanges();
			return new CommonResponse("course deleted..!!", true);
		}
		catch(Exception ex)
		{
			return new CommonResponse($"cannot delete course because: {ex.Message}", false);
		}
	}

	public async Task<IEnumerable<GetCourseWithCategoryDto>> GetAllToShowAsync(int pageNumber)
	{
		var courses = await _unitOfWork.CourseRepo.GetAllAsync(pageNumber);
		if(courses is null)
		{
			return null!;
		}

		courses = courses.Where(c => c.IsDeleted == false).ToList();

		return courses.Select(course => CourseMapper.ToGetDto(course));
		
	}

	public async Task<IEnumerable<GetCourseWithCategoryDto>> GetAllWithQueryAsync(CourseQueryHandler query)
	{
		var courses = await _unitOfWork.CourseRepo.GetAllWithQueryAsync(query);
		if (courses is null)
		{
			return null!;
		}

		return courses.Select(course => CourseMapper.ToGetDto(course));
	}

	public async Task<GetCourseWithIncludesDto> GetByIdWithIncludesAsync(Guid id)
	{
		var course = await _unitOfWork.CourseRepo.GetByIdWithIncludesAsync(id);
		if(course is null)
		{
			return null!;
		}

		return CourseMapper.ToGetWithIncludesDto(course, _unitOfWork.StudentCourseRepo.GetStudentsNumber(course.Id));
	}

	public async Task<IEnumerable<GetCourseWithCategoryDto>> GetInstructorCoursesAsync(CourseQueryHandler query, string userId)
	{
		var instructor = await _unitOfWork.InstructorRepo.GetByRefIdAsync(userId);
		var courses = await _unitOfWork.CourseRepo.GetInstructorCoursesAsync(query, instructor.Id);
		if (courses is null)
		{
			return null!;
		}

		return courses.Select(course => CourseMapper.ToGetDto(course));
	}

	public async Task<IEnumerable<GetCourseWithCategoryDto>> GetStudentCoursesAsync(CourseQueryHandler query, string userId)
	{
		var student = await _unitOfWork.StudentRepo.GetByRefIdAsync(userId);
		var courses = await _unitOfWork.StudentCourseRepo.GetStudentCoursesAsync(query, student.Id);
		if (courses is null)
		{
			return null!;
		}

		return courses.Select(course => CourseMapper.ToGetDto(course));
	}

	public async Task<CommonResponse> MarkCourseAsDeletedAsync(Guid id)
	{
		var courseToDelete = await _unitOfWork.CourseRepo.GetByIdAsync(id);
		if (courseToDelete is null || courseToDelete.IsDeleted)
		{
			return new CommonResponse("course not found..!!", false);
		}

		try
		{
			courseToDelete.IsDeleted = true;
			_unitOfWork.CourseRepo.Update(courseToDelete);
			_unitOfWork.CourseRepo.SaveChanges();
			return new CommonResponse("course deleted..!!", true);
		}
		catch (Exception ex)
		{
			return new CommonResponse($"cannot delete course because: {ex.Message}", false);
		}
	}

	public async Task<CommonResponse> UpdateCourseAsync(Guid id, UpdateCourseDto model)
	{
		var courseToUpdate = await _unitOfWork.CourseRepo.GetByIdWithIncludesAsync(id);
		if(courseToUpdate is null || courseToUpdate.IsDeleted)
		{
			return new CommonResponse("course not found..!!", false);
		}

		try
		{
			courseToUpdate.Title = model.Title;
			courseToUpdate.Description = model.Description;
			courseToUpdate.ImgeUrl = model.ImgeUrl;
			courseToUpdate.Details = model.Details;
			courseToUpdate.OfferOrice = model.OfferOrice;
			courseToUpdate.OriginalOrice = model.OriginalOrice;

			courseToUpdate.Advanteges?.Clear();
			var advs = model.Advanteges?.Select(adv => new CourseAdvantage
			{
				Advantege = adv,
				CourseId = courseToUpdate.Id,
				Id = Guid.NewGuid(),

			}).ToList();

			//> update advanteges first, then update the course to prvent issues
			await _unitOfWork.CourseAdvantegeRepo.CreateRangeAsync(advs!);

			_unitOfWork.CourseRepo.Update(courseToUpdate);
			await _unitOfWork.CourseRepo.SaveChangesAsync();
			return new CommonResponse("course updated...!!", true);
		}
		catch (Exception ex)
		{
			return new CommonResponse($"cannot update course right now beause: {ex.Message}", false);
		}
	}

	public async Task<IEnumerable<GetCourseToAdminDashDto>> GetCoursesOfLastMonthAsync(int pageNumber)
	{
		var orders = await _unitOfWork.CourseRepo.GetCoursesOfLastMonthAsync(pageNumber);

		return orders.Select(order => new GetCourseToAdminDashDto
		{
			Description = order.Description,
			InstructorEmail = order.Instructor?.AppUser?.Email ?? "NA",
			OfferOrice = order.OfferOrice,
			OriginalOrice = order.OriginalOrice,
			StudentsNumber = order.StudentsNumber,
			Title = order.Title

		}).ToList();
	}

	public async Task<IEnumerable<GetCourseToAdminDashDto>> GetCoursesOfLastYearAsync(int pageNumber)
	{
		var orders = await _unitOfWork.CourseRepo.GetCoursesOfLastYearAsync(pageNumber);

		return orders.Select(order => new GetCourseToAdminDashDto
		{
			Description = order.Description,
			InstructorEmail = order.Instructor?.AppUser?.Email ?? "NA",
			OfferOrice = order.OfferOrice,
			OriginalOrice = order.OriginalOrice,
			StudentsNumber = order.StudentsNumber,
			Title = order.Title

		}).ToList();
	}

	public int GetCoursesCountOfLastYear()
	{
		return _unitOfWork.CourseRepo.GetCoursesCountOfLastYear();
	}

	public int GetCoursesCountOfLastMonth()
	{
		return _unitOfWork.CourseRepo.GetCoursesCountOfLastMonth();
	}

	public int GetCoursesCount()
	{
		return _unitOfWork.CourseRepo.GetCoursesCount();
	}
}
