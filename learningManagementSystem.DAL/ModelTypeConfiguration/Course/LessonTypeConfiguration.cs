


namespace learningManagementSystem.DAL.ModelTypeConfiguration;

public class LessonTypeConfiguration : IEntityTypeConfiguration<Lesson>
{
	public void Configure(EntityTypeBuilder<Lesson> modelBuilder)
	{
		modelBuilder.HasKey(L => L.Id);
		modelBuilder.Property(L => L.Name).IsRequired().HasMaxLength(80);

		modelBuilder.HasMany(L => L.Videos)
			.WithOne(V =>  V.Lesson)
			.HasForeignKey(V => V.LessonId)
			.OnDelete(DeleteBehavior.Cascade);
	}
}
