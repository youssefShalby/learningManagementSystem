


namespace learningManagementSystem.DAL.Repositories;

public class CourseRepo : GenericRepo<Course>, ICourseRepo
{
	private readonly AppDbContext _context;
	private readonly IConfiguration _configuration;

	public CourseRepo(AppDbContext context, IConfiguration configuration) : base(context, configuration)
	{
		_context = context;
		_configuration = configuration;
	}

	public async Task<IEnumerable<Course>> GetAllWithQueryAsync(CourseQueryHandler query)
	{
		var courses = _context.Set<Course>().Where(C => C.IsDeleted == false).AsQueryable();

		if (query.PageSize <= 0)
		{
			query.PageSize = _configuration["CustomConfiguration:PageSize"] is null ? 10 : 
				int.Parse(_configuration["CustomConfiguration:PageSize"]);
		}

		//> filter
		if (!string.IsNullOrEmpty(query.Title))
		{
			courses = courses.Where(c => c.Title.Contains(query.Title));
		}

		//> sort
		if (!string.IsNullOrEmpty(query.SortBy))
		{
			if (query.SortBy.Equals("price"))
			{
				courses = query.IsDescending ? courses.OrderByDescending(c => c.OfferOrice) : courses.OrderBy(c => c.OfferOrice);
			}
			if (query.SortBy.Equals("date"))
			{
				courses = query.IsDescending ? courses.OrderByDescending(c => c.CreatedAt) : courses.OrderBy(c => c.CreatedAt);
			}
			if (query.SortBy.Equals("name"))
			{
				courses = query.IsDescending ? courses.OrderByDescending(c => c.Title) : courses.OrderBy(c => c.Title);
			}
		}

		//> pagination
		courses = courses.Skip((query.PageNumber - 1) * query.PageSize).Take(query.PageSize);
		return await courses.ToListAsync();
	}

	public async Task<Course> GetByIdWithIncludesAsync(Guid id)
	{
		return await _context.Set<Course>()
			.Include(c => c.Comments!).ThenInclude(c => c.AppUser)
			.Include(c => c.Advanteges)
			.Include(c => c.Category)
			.Include(c => c.Instructor).ThenInclude(I => I!.AppUser)
			.Include(c => c.Lessons!).ThenInclude(L => L.Videos)
			.FirstOrDefaultAsync(c => c.Id == id) ?? null!;
	}

	public int GetInstructorCoursesCount(Guid instructordId)
	{
		return _context.Courses.Where(C => C.Instructor!.Id == instructordId && C.IsDeleted == false).Count();
	}

	public async Task<IEnumerable<Course>> GetCoursesOfLastMonthAsync(int pageNumber)
	{
		var courses = _context.Set<Course>()
			.Include(C => C.Instructor).ThenInclude(I => I!.AppUser)
			.Where(c => c.CreatedAt >= DateTime.Now.AddMonths(-1)).AsQueryable();

		var pageSize = int.Parse(_configuration["CustomConfiguration:PageSize"] ?? "10");

		courses = courses.Skip((pageNumber - 1) * pageSize).Take(pageSize);
		return await courses.ToListAsync();
	}

	public async Task<IEnumerable<Course>> GetCoursesOfLastYearAsync(int pageNumber)
	{
		var courses = _context.Set<Course>()
			.Include(C => C.Instructor).ThenInclude(I => I!.AppUser)
			.Where(c => c.CreatedAt >= DateTime.Now.AddYears(-1)).AsQueryable();

		var pageSize = int.Parse(_configuration["CustomConfiguration:PageSize"] ?? "10");

		courses = courses.Skip((pageNumber - 1) * pageSize).Take(pageSize);
		return await courses.ToListAsync();
	}

	public int GetCoursesCountOfLastYear()
	{
		return _context.Set<Course>()
			.Where(c => c.CreatedAt >= DateTime.Now.AddYears(-1)).Count();
	}

	public int GetCoursesCountOfLastMonth()
	{
		return _context.Set<Course>()
			.Where(c => c.CreatedAt >= DateTime.Now.AddMonths(-1)).Count();
	}

	public int GetCoursesCount()
	{
		return _context.Courses.Count();
	}

	public async Task<IEnumerable<Course>> GetInstructorCoursesAsync(CourseQueryHandler query, Guid instructorId)
	{
		try
		{
			var portfolio = _context.Courses.Where(C => C.Instructor!.Id == instructorId && C.IsDeleted == false);

			if (!string.IsNullOrEmpty(query.Title))
			{
				portfolio = portfolio.Where(c => c.Title.Contains(query.Title));
			}

			if (!string.IsNullOrEmpty(query.SortBy))
			{
				if (query.SortBy.Equals("date"))
				{
					portfolio = query.IsDescending ? portfolio.OrderByDescending(c => c.CreatedAt) : portfolio.OrderBy(c => c.CreatedAt);
				}

				if (query.SortBy.Equals("price"))
				{
					portfolio = query.IsDescending ? portfolio.OrderByDescending(c => c.OfferOrice) : portfolio.OrderBy(c => c.OfferOrice);
				}

				if (query.SortBy.Equals("title"))
				{
					portfolio = query.IsDescending ? portfolio.OrderByDescending(c => c.Title) : portfolio.OrderBy(c => c.Title);
				}
			}

			if (query.PageSize <= 0)
			{
				query.PageSize = _configuration["CustomConfiguration:PageSize"] is null ? 10 : int.Parse(_configuration["CustomConfiguration:PageSize"]);
			}

			portfolio = portfolio.Skip((query.PageNumber - 1) * query.PageSize).Take(query.PageSize);

			var courses = await portfolio.Select(course => new Course
			{
				Id = course.Id,
				Advanteges = course!.Advanteges,
				Category = course.Category,
				Title = course.Title,
				StudentsNumber = course.StudentsNumber,
				OriginalOrice = course.OriginalOrice,
				OfferOrice = course.OfferOrice,
				IsDeleted = course.IsDeleted,
				Lessons = course.Lessons,
				Details = course.Details,
				Description = course.Description,
				CreatedAt = course.CreatedAt,
				Comments = course.Comments,
				CategoryId = course.CategoryId


			}).ToListAsync();

			return courses;
		}
		catch
		{
			return null!;
		}
	}

	public bool IsInstructorOfCourse(Guid userId, Guid courseId)
	{
		return  _context.Courses.Any(c => c.Id == courseId && c.InstructorId == userId);
	}

	public async Task UnlockCourseVideosAsync(Guid id)
	{
		var course = await GetByIdWithIncludesAsync(id);

		if(course?.Lessons is not null)
		{
			foreach (var lesson in course?.Lessons!)
			{
				if (lesson.Videos is null) continue;
				foreach (var video in lesson.Videos)
				{
					video.IsLocked = false;
				}
			}
		}

		//> save changes after it direct
		await _context.SaveChangesAsync();
	}
}
