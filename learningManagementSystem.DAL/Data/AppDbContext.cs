
namespace learningManagementSystem.DAL;

public class AppDbContext : IdentityDbContext<ApplicationUser>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        //> pass the options to base class(IdentityDbContext<AppUser>)
    }

	//> Models which mapped to tables
	public DbSet<Category> Categories => Set<Category>();
	public DbSet<Course> Courses => Set<Course>();
	public DbSet<CourseAdvantage> CourseAdvantages => Set<CourseAdvantage>();
	public DbSet<Lesson> Lessons => Set<Lesson>();
	public DbSet<StudentCourse> StudentCourses => Set<StudentCourse>();
	public DbSet<Video> Videos => Set<Video>();
	public DbSet<Order> Orders => Set<Order>();
	public DbSet<Comment> Comments => Set<Comment>();
	public DbSet<CommentReply> CommentReplies => Set<CommentReply>();
	public DbSet<Student> Students => Set<Student>();
	public DbSet<Instructor> Instructors => Set<Instructor>();
	public DbSet<CoursePayment> CoursePayments=> Set<CoursePayment>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		base.OnConfiguring(optionsBuilder);

		//optionsBuilder.EnableSensitiveDataLogging();
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);

		//> call the references of extrnal configurations
		new CommentReplyTypeConfiguration().Configure(modelBuilder.Entity<CommentReply>());
		new CommentTypeConfiguration().Configure(modelBuilder.Entity<Comment>());
		new AppUserTypeConfiguration().Configure(modelBuilder.Entity<ApplicationUser>());
		new CategoryTypeConfiguration().Configure(modelBuilder.Entity<Category>());
		new CourseTypeConfiguration().Configure(modelBuilder.Entity<Course>());
		new CourseAdvantegeTypeConfguration().Configure(modelBuilder.Entity<CourseAdvantage>());
		new LessonTypeConfiguration().Configure(modelBuilder.Entity<Lesson>());
		new VideoTypeConfiguration().Configure(modelBuilder.Entity<Video>());
		new StudentCourseTypeConfiguration().Configure(modelBuilder.Entity<StudentCourse>());
		new CoursePaymentTypeConfiguration().Configure(modelBuilder.Entity<CoursePayment>());


		//> many to many relationships

	}
}
