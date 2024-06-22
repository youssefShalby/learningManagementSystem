



namespace learningManagementSystem.DAL.Repositories;

public class StudentCourseRepo : GenericRepo<StudentCourse>, IStudentCourseRepo
{
	private readonly AppDbContext _context;

	public StudentCourseRepo(AppDbContext context, IConfiguration configuration) : base(context, configuration)
    {
		_context = context;
	}


	public async Task<IEnumerable<Course>> GetStudentCoursesAsync(ApplicationUser user)
	{
		try
		{
			var portfolio = _context.StudentCourses.Where(SC => SC.Student!.UserRefId == user.Id);
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

	//> Enroll User to course => Handle in BLL layer by Create method which in Generic repo
	//> Out User from course => Handle in BLL layer by Delete method which in Generic repo
}
