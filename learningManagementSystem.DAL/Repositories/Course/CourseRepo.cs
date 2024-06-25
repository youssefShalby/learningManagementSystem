﻿


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
		query.PageSize = int.Parse(_configuration["CustomConfiguration:CustomConfiguration"]);

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
			.Include(c => c.Comments)
			.Include(c => c.Advanteges)
			.Include(c => c.Lessons!).ThenInclude(L => L.Videos)
			.FirstOrDefaultAsync(c => c.Id == id) ?? null!;
	}

	public async Task<IEnumerable<Course>> GetInstructorCoursesAsync(ApplicationUser user)
	{
		try
		{
			var portfolio = _context.Courses.Where(C => C.Instructor!.UserRefId == user.Id);
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
}