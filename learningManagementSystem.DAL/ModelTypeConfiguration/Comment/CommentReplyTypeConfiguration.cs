


namespace learningManagementSystem.DAL.ModelTypeConfiguration;

public class CommentReplyTypeConfiguration : IEntityTypeConfiguration<CommentReply>
{
	public void Configure(EntityTypeBuilder<CommentReply> modelBuilder)
	{
		modelBuilder.HasKey(C => C.Id);
		modelBuilder.Property(C => C.Content).IsRequired().HasMaxLength(250);

		modelBuilder.HasOne(c => c.AppUser).WithOne().HasForeignKey<CommentReply>(c => c.UserId).OnDelete(DeleteBehavior.NoAction);
	}
}
