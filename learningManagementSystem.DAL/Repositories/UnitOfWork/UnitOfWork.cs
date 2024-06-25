


namespace learningManagementSystem.DAL.Repositories;

public class UnitOfWork : IUnitOfWork
{
	public ICategoryRepo CategoryRepo { get; }
	public ICommentRepo CommentRepo { get; }
	public ICourseRepo CourseRepo { get; }
	public ILessonRepo LessonRepo { get; }
	public IOrderRepo OrderRepo { get; }
	public IStudentRepo StudentRepo { get; }
	public IInstructorRepo InstructorRepo { get; }
	public IStudentCourseRepo StudentCourseRepo { get; }
	public RoleManager<IdentityRole> RoleManager { get; }
	public UserManager<ApplicationUser> UserManager { get; }
	public IConfiguration Configuration { get; }

    public UnitOfWork(
			ICategoryRepo categoryRepo, ICommentRepo commentRepo, ICourseRepo courseRepo, ILessonRepo lessonRepo,
			IOrderRepo orderRepo, IStudentCourseRepo studentCourseRepo,
			RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager, IConfiguration configuration, 
			IStudentRepo studentRepo, IInstructorRepo instructorRepo
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
    }
}
