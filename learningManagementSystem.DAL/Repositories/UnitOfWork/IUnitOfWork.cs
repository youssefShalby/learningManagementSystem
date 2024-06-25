
namespace learningManagementSystem.DAL.Repositories;

public interface IUnitOfWork
{
    ICategoryRepo CategoryRepo { get; }
    ICommentRepo CommentRepo { get; }
    ICourseRepo CourseRepo { get; }
    ILessonRepo LessonRepo { get; }
    IOrderRepo OrderRepo { get; }
    IStudentRepo StudentRepo { get; }
    IInstructorRepo InstructorRepo { get; }

    IStudentCourseRepo StudentCourseRepo { get; }

	RoleManager<IdentityRole> RoleManager { get; }

	UserManager<ApplicationUser> UserManager { get; }
	IConfiguration Configuration { get; }
}
