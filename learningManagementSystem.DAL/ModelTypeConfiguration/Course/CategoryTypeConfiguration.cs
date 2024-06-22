


namespace learningManagementSystem.DAL.ModelTypeConfiguration;

public class CategoryTypeConfiguration : IEntityTypeConfiguration<Category>
{
	public void Configure(EntityTypeBuilder<Category> modelBuilder)
	{
		modelBuilder.HasKey(C => C.Id);
		modelBuilder.Property(C => C.Name).IsRequired().HasMaxLength(100);

		modelBuilder.HasMany(cat => cat.Courses)
			.WithOne(c => c.Category)
			.HasForeignKey(c => c.CategoryId)
			.OnDelete(DeleteBehavior.SetNull);
	}
}
