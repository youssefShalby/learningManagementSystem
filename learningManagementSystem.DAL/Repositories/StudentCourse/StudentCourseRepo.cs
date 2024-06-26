



namespace learningManagementSystem.DAL.Repositories;

public class StudentCourseRepo : GenericRepo<StudentCourse>, IStudentCourseRepo
{
	private readonly AppDbContext _context;
	private readonly IConfiguration _configuration;

	public StudentCourseRepo(AppDbContext context, IConfiguration configuration) : base(context, configuration)
    {
		_context = context;
		_configuration = configuration;
	}


	public async Task<IEnumerable<Course>> GetStudentCoursesAsync(CourseQueryHandler query, string userId)
	{
		try
		{
			var portfolio = _context.StudentCourses.Where(SC => SC.Student!.Id.ToString() == userId && SC.Course!.IsDeleted == false);

			if (!string.IsNullOrEmpty(query.Title))
			{
				portfolio = portfolio.Where(c => c.Course!.Title.Contains(query.Title));
			}

			if (!string.IsNullOrEmpty(query.SortBy))
			{
				if (query.SortBy.Equals("date"))
				{
					portfolio = query.IsDescending ? portfolio.OrderByDescending(c => c.Course!.CreatedAt) : portfolio.OrderBy(c => c.Course!.CreatedAt);
				}

				if (query.SortBy.Equals("price"))
				{
					portfolio = query.IsDescending ? portfolio.OrderByDescending(c => c.Course!.OfferOrice) : portfolio.OrderBy(c => c.Course!.OfferOrice);
				}

				if (query.SortBy.Equals("title"))
				{
					portfolio = query.IsDescending ? portfolio.OrderByDescending(c => c.Course!.Title) : portfolio.OrderBy(c => c.Course!.Title);
				}
			}

			if (query.PageSize <= 0)
			{
				query.PageSize = _configuration["CustomConfiguration:PageSize"] is null ? 10 : int.Parse(_configuration["CustomConfiguration:PageSize"]);
			}

			var courses = await portfolio.Select(course => new Course
			{
				Id = course.CourseId,
				Advanteges = course.Course!.Advanteges,
				Category = course.Course.Category,
				Title = course.Course.Title,
				StudentsNumber = course.Course.StudentsNumber,
				OriginalOrice = course.Course.OriginalOrice,
				OfferOrice = course.Course.OfferOrice,
				IsDeleted = course.Course.IsDeleted,
				Lessons = course.Course.Lessons,
				Details = course.Course.Details,
				Description = course.Course.Description,
				CreatedAt = course.Course.CreatedAt,
				Comments = course.Course.Comments,
				CategoryId = course.Course.CategoryId

			}).ToListAsync();

			return courses;
		}
		catch (Exception)
		{
			return null!;
		}
	}

	public int GetStudentsNumber(Guid courseId)
	{
		try
		{
			var courseRepeat = _context.StudentCourses.Where(SC => SC.CourseId == courseId && SC.StudentId != null);
			return courseRepeat.Count();
		}
		catch
		{
			return 0;
		}
	}

	//> Enroll User to course => Handle in BLL layer by Create method which in Generic repo
	//> Out User from course => Handle in BLL layer by Delete method which in Generic repo
}
