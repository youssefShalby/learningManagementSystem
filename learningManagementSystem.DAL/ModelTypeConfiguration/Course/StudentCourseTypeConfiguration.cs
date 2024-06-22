

namespace learningManagementSystem.DAL.ModelTypeConfiguration;

public class StudentCourseTypeConfiguration : IEntityTypeConfiguration<StudentCourse>
{
	public void Configure(EntityTypeBuilder<StudentCourse> modelBuilder)
	{
		modelBuilder.HasKey(US => new { US.CourseId, US.StudentId });
	}
}
