

namespace learningManagementSystem.API.Filter;

public class AccessVideosFilter : IAsyncActionFilter
{
	private readonly IUnitOfWork _unitOfWork;

	public AccessVideosFilter(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
	{
		var videoId = Guid.Parse(context.RouteData.Values["id"]?.ToString()!);
		var video = await _unitOfWork.VideoRepo.GetByIdAsync(videoId);
		if (video is null)
		{
			context.Result = new BadRequestObjectResult("Video not found..!!");
			return;
		}

		var userEmail = context.HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
		var user = await _unitOfWork.UserManager.FindByEmailAsync(userEmail);
		if (user is null)
		{
			context.Result = new BadRequestObjectResult("Something went wrong..!!");
			return;
		}

		var Instructor = await _unitOfWork.InstructorRepo.GetByRefIdAsync(user.Id);
		var student = await _unitOfWork.StudentRepo.GetByRefIdAsync(user.Id);
		if(student is null && Instructor is null)
		{
			context.Result = new BadRequestObjectResult("Something went wrong..!!");
			return;
		}

		var lesson = await _unitOfWork.LessonRepo.GetByIdAsync(video.LessonId);
		if (lesson is null)
		{
			context.Result = new BadRequestObjectResult("Something went wrong..!!");
			return;
		}

		var course = await _unitOfWork.CourseRepo.GetByIdAsync(lesson.CourseId);
		if (course is null)
		{
			context.Result = new BadRequestObjectResult("Something went wrong..!!");
			return;
		}

		bool isInstructor = false;
		if (Instructor is not null)
		{
			isInstructor = _unitOfWork.CourseRepo.IsInstructorOfCourse(Instructor.Id, course.Id);
		}

		bool hasPurchased = false;
		if(student is not null)
		{
			hasPurchased = _unitOfWork.OrderRepo.HasPurchasedCourse(student.Id, course.Id);
		}

		if (!(isInstructor || hasPurchased))
		{
			context.Result = new ForbidResult();
			return;
		}

		await next(); // Proceed to the next middleware or action
	}
}
