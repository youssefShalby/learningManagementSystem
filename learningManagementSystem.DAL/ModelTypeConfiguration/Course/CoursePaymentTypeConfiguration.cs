


namespace learningManagementSystem.DAL.ModelTypeConfiguration;

public class CoursePaymentTypeConfiguration : IEntityTypeConfiguration<CoursePayment>
{
	public void Configure(EntityTypeBuilder<CoursePayment> modelBuilder)
	{
		modelBuilder.HasKey(CP => CP.Id);
	}
}
