

namespace learningManagementSystem.DAL.ModelTypeConfiguration;

public class AppUserTypeConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
	public void Configure(EntityTypeBuilder<ApplicationUser> modelBuilder)
	{
		modelBuilder.HasKey(AU => AU.Id);
		modelBuilder.Property(U => U.DisplayName).IsRequired().HasMaxLength(80);

		modelBuilder.HasOne(U => U.Address).WithOne().HasForeignKey<Address>(A => A.AppUserId);

	}
}
