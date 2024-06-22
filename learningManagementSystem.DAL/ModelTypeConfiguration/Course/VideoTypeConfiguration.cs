
namespace learningManagementSystem.DAL.ModelTypeConfiguration;

public class VideoTypeConfiguration : IEntityTypeConfiguration<Video>
{
	public void Configure(EntityTypeBuilder<Video> modelBuilder)
	{
		modelBuilder.HasKey(V => V.Id);
		modelBuilder.Property(V => V.Title).IsRequired().HasMaxLength(100);
		modelBuilder.Property(V => V.Description).IsRequired().HasMaxLength(250);
		modelBuilder.Property(V => V.FileFormat).IsRequired().HasMaxLength(20);
		modelBuilder.Property(V => V.Length).IsRequired();


		//> Here make the DeleteBehavior Restricted because EF core not allow cycles or multiple cascade paths
		//> the solution to do that, In delete method will delete related data first and then delete the Video
		modelBuilder.HasMany(V => V.Comments)
			.WithOne(C => C.Video)
			.HasForeignKey(C => C.VideoId)
			.OnDelete(DeleteBehavior.Restrict);
	}
}

