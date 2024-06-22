


namespace learningManagementSystem.DAL.ModelTypeConfiguration;

public class CourseAdvantegeTypeConfguration : IEntityTypeConfiguration<CourseAdvantage>
{
	public void Configure(EntityTypeBuilder<CourseAdvantage> modelBuilder)
	{
		modelBuilder.HasKey(CA => CA.Id);
		modelBuilder.Property(CA => CA.Advantege).IsRequired().HasMaxLength(100);
	}
}
