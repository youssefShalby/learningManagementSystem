


namespace learningManagementSystem.DAL.ModelTypeConfiguration;

public class InstructorTypeConfiguration : IEntityTypeConfiguration<Instructor>
{
	public void Configure(EntityTypeBuilder<Instructor> modelBuilder)
	{
		modelBuilder.HasKey(I => I.Id);

		modelBuilder.HasOne(I => I.AppUser).WithOne().HasForeignKey<Instructor>(I => I.UserRefId);

		modelBuilder.HasMany(I => I.Courses)
			.WithOne(C => C.Instructor)
			.HasForeignKey(I => I.Id)
			.OnDelete(DeleteBehavior.Cascade);
		

	}
}
