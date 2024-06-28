
namespace learningManagementSystem.DAL.Repositories;

public interface IUnitOfWork
{
    ICategoryRepo CategoryRepo { get; }
    ICommentRepo CommentRepo { get; }
    ICommentRelyRepo CommentRelyRepo { get; }
    ICourseRepo CourseRepo { get; }
    ICourseDashboardRepo CourseDashboardRepo { get; }
    ICoursePaymentRepo CoursePaymentRepo { get; }
    ILessonRepo LessonRepo { get; }
    IVideoRepo VideoRepo { get; }
    IOrderRepo OrderRepo { get; }
    IOrderDashboardRepo OrderDashboardRepo { get; }
    IStudentRepo StudentRepo { get; }
    IInstructorRepo InstructorRepo { get; }
    ICourseAdvantegeRepo CourseAdvantegeRepo { get; }

    IStudentCourseRepo StudentCourseRepo { get; }

	RoleManager<IdentityRole> RoleManager { get; }

	UserManager<ApplicationUser> UserManager { get; }
	IConfiguration Configuration { get; }
}
