

namespace learningManagementSystem.BLL.Mappers;

public class LessonMapper
{
	public static GetLessonToCourseDto ToGetForCourse(Lesson lesson)
	{
		return new GetLessonToCourseDto
		{
			CourseId = lesson.CourseId,
			Duration = lesson.Duration,
			Name = lesson.Name,
			VideosNumber = lesson.Videos?.Count ?? 0,
			Videos = lesson.Videos?.Select(vid => VideoMapper.ToGetForLessonDto(vid)).ToList(),
		};
	}
}
