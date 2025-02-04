﻿

namespace learningManagementSystem.DAL.ModelTypeConfiguration;

public class CommentTypeConfiguration : IEntityTypeConfiguration<Comment>
{
	public void Configure(EntityTypeBuilder<Comment> modelBuilder)
	{
		modelBuilder.HasKey(C => C.Id);
		modelBuilder.Property(C => C.Content).IsRequired().HasMaxLength(250);
		modelBuilder.Property(C => C.VideoId).IsRequired(false);
		modelBuilder.Property(C => C.CourseId).IsRequired(false);
		modelBuilder.Property(C => C.UserId).IsRequired(false);

		modelBuilder.HasIndex(c => c.UserId).IsUnique(false);
		modelBuilder.HasIndex(c => c.VideoId).IsUnique(false);
		modelBuilder.HasIndex(c => c.CourseId).IsUnique(false);

		modelBuilder.HasOne(c => c.AppUser).WithOne().HasForeignKey<Comment>(c => c.UserId).OnDelete(DeleteBehavior.NoAction);

		modelBuilder.HasMany(com => com.Replies)
			.WithOne(rep => rep.Comment)
			.HasForeignKey(rep => rep.CommentId)
			.OnDelete(DeleteBehavior.Cascade);

	}
}
