
namespace learningManagementSystem.DAL.Repositories;

public interface IUnitOfWork
{
    ICategoryRepo CategoryRepo { get; }
    ICommentRepo CommentRepo { get; }
    ICommentRelyRepo CommentRelyRepo { get; }
    ICourseRepo CourseRepo { get; }
    ICoursePaymentRepo CoursePaymentRepo { get; }
    ILessonRepo LessonRepo { get; }
    IVideoRepo VideoRepo { get; }
    IOrderRepo OrderRepo { get; }
    IStudentRepo StudentRepo { get; }
    IInstructorRepo InstructorRepo { get; }
    ICourseAdvantegeRepo CourseAdvantegeRepo { get; }

    IStudentCourseRepo StudentCourseRepo { get; }

	RoleManager<IdentityRole> RoleManager { get; }

	UserManager<ApplicationUser> UserManager { get; }
	IConfiguration Configuration { get; }
}
