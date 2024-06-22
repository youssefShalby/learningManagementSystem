


namespace learningManagementSystem.DAL.ModelTypeConfiguration;

public class StudentTypeConfiguration : IEntityTypeConfiguration<Student>
{
	public void Configure(EntityTypeBuilder<Student> modelBuilder)
	{
		modelBuilder.HasKey(S => S.Id);
		modelBuilder.HasOne(I => I.AppUser).WithOne().HasForeignKey<Instructor>(I => I.UserRefId);

		modelBuilder.HasMany(U => U.StudentCourses)
			.WithOne(SC => SC.Student)
			.HasForeignKey(US => US.StudentId);
	}
}
