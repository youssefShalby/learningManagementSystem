

namespace learningManagementSystem.DAL.ModelTypeConfiguration;

public class CourseTypeConfiguration : IEntityTypeConfiguration<Course>
{
	public void Configure(EntityTypeBuilder<Course> modelBuilder)
	{
		modelBuilder.HasKey(C => C.Id);
		modelBuilder.Property(C => C.Title).IsRequired().HasMaxLength(100);
		modelBuilder.Property(C => C.Description).HasMaxLength(150);
		modelBuilder.Property(C => C.Details).HasMaxLength(250);
		modelBuilder.Property(C => C.Details).HasMaxLength(250);
		modelBuilder.Property(C => C.OriginalOrice).HasColumnType("decimal(18, 2)");
		modelBuilder.Property(C => C.OfferOrice).HasColumnType("decimal(18, 2)");

		modelBuilder.HasMany(C => C.Lessons)
			.WithOne(L => L.Course)
			.HasForeignKey(L => L.CourseId)
			.OnDelete(DeleteBehavior.Cascade);

		modelBuilder.HasMany(C => C.Comments)
			.WithOne(com => com.Course)
			.HasForeignKey(com => com.CourseId)
			.OnDelete(DeleteBehavior.Cascade);
		
		modelBuilder.HasMany(C => C.Advanteges)
			.WithOne(A => A.Course)
			.HasForeignKey(A => A.CourseId)
			.OnDelete(DeleteBehavior.Cascade);

		modelBuilder.HasMany(C => C.StudentCourses)
			.WithOne(SC => SC.Course)
			.HasForeignKey(SC => SC.CourseId);
	}
}
