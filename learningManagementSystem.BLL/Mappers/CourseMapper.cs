

namespace learningManagementSystem.BLL.Mappers;

public class CourseMapper
{
	public static GetCourseWithCategoryDto ToGetDto(Course course, int studentsNumber)
	{
		return new GetCourseWithCategoryDto
		{
			Description = course.Description,
			ImgeUrl = course.ImgeUrl,
			OfferOrice = course.OfferOrice,
			OriginalOrice = course.OriginalOrice,
			StudentsNumber = studentsNumber,
			Title = course.Title,
		};
	}

	public static GetCourseWithIncludesDto ToGetWithIncludesDto(Course course, int studentNumbers)
	{
		return new GetCourseWithIncludesDto
		{
			Description = course.Description,
			ImgeUrl = course.ImgeUrl,
			OfferOrice = course.OfferOrice,
			OriginalOrice = course.OriginalOrice,
			StudentsNumber = studentNumbers,
			Title = course.Title,
			CategoryName = course.Category?.Name ?? "NA",
			Details = course.Details,
			InstructorName = course.Instructor?.AppUser?.DisplayName ?? "NA",
			CreatedAt = course.CreatedAt,
			Comments = course.Comments?.Select(com => CommentMapper.ToGetForCourse(com)).ToList(),
			Advanteges = course.Advanteges?.Select(adv => adv.Advantege).ToList(),
			Lessons = course.Lessons?.Select(lesson => LessonMapper.ToGetForCourse(lesson)).ToList(),
		};
	}

	public static Course FromCreateDto(CreateCourseDto course)
	{
		var newCourse = new Course
		{
			Title = course.Title,
			Description = course.Description,
			CategoryId = course.CategoryId,
			CreatedAt = course.CreatedAt,
			ImgeUrl = course.ImgeUrl,
			Details = course.Details,
			OfferOrice = course.OfferOrice,
			OriginalOrice = course.OriginalOrice,
			Id = Guid.NewGuid(),
			InstructorId = course.InstructorId,
		};

		newCourse.Advanteges = course.Advanteges?.Select(adv => new CourseAdvantage
		{
			Advantege = adv,
			CourseId = newCourse.Id,
			Id = Guid.NewGuid(),

		}).ToList();

		return newCourse;
	}

	public static Course FromUpdateDto(UpdateCourseDto course)
	{
		var newCourse = new Course
		{
			Title = course.Title,
			Description = course.Description,
			ImgeUrl = course.ImgeUrl,
			Details = course.Details,
			OfferOrice = course.OfferOrice,
			OriginalOrice = course.OriginalOrice,
		};

		newCourse.Advanteges?.Clear();
		newCourse.Advanteges = course.Advanteges?.Select(adv => new CourseAdvantage
		{
			Advantege = adv,
			CourseId = newCourse.Id,
			Id = Guid.NewGuid(),

		}).ToList();



		return newCourse;
	}
}
