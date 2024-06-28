


namespace learningManagementSystem.DAL.Repositories;

public class UnitOfWork : IUnitOfWork
{
	public ICategoryRepo CategoryRepo { get; }
	public ICommentRepo CommentRepo { get; }
	public ICommentRelyRepo CommentRelyRepo { get; }
	public ICourseRepo CourseRepo { get; }
	public ICourseDashboardRepo CourseDashboardRepo { get; }
	public ICoursePaymentRepo CoursePaymentRepo { get; }
	public ILessonRepo LessonRepo { get; }
	public IVideoRepo VideoRepo { get; }
	public IOrderRepo OrderRepo { get; }
	public IOrderDashboardRepo OrderDashboardRepo { get; }
	public IStudentRepo StudentRepo { get; }
	public IInstructorRepo InstructorRepo { get; }
	public ICourseAdvantegeRepo CourseAdvantegeRepo { get; }
	public IStudentCourseRepo StudentCourseRepo { get; }
	public RoleManager<IdentityRole> RoleManager { get; }
	public UserManager<ApplicationUser> UserManager { get; }
	public IConfiguration Configuration { get; }

    public UnitOfWork(
			ICategoryRepo categoryRepo, ICommentRepo commentRepo, ICourseRepo courseRepo, ILessonRepo lessonRepo,
			IOrderRepo orderRepo, IStudentCourseRepo studentCourseRepo,
			RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager, IConfiguration configuration,
			IStudentRepo studentRepo, IInstructorRepo instructorRepo, ICourseAdvantegeRepo courseAdvantegeRepo, IVideoRepo videoRepo,
			ICommentRelyRepo commentRelyRepo, ICoursePaymentRepo coursePaymentRepo, IOrderDashboardRepo orderDashboardRepo,	
			ICourseDashboardRepo courseDashboardRepo
		)
	{
		CategoryRepo = categoryRepo;
		CommentRepo = commentRepo;
		CourseRepo = courseRepo;
		LessonRepo = lessonRepo;
		OrderRepo = orderRepo;
		StudentCourseRepo = studentCourseRepo;
		RoleManager = roleManager;
		UserManager = userManager;
		Configuration = configuration;
		StudentRepo = studentRepo;
		InstructorRepo = instructorRepo;
		CourseAdvantegeRepo = courseAdvantegeRepo;
		VideoRepo = videoRepo;
		CommentRelyRepo = commentRelyRepo;
		CoursePaymentRepo = coursePaymentRepo;
		OrderDashboardRepo = orderDashboardRepo;
		CourseDashboardRepo = courseDashboardRepo;
	}
}
