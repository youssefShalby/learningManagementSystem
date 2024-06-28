

namespace learningManagementSystem.DAL.ModelTypeConfiguration;

public class AppUserTypeConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
	public void Configure(EntityTypeBuilder<ApplicationUser> modelBuilder)
	{
		modelBuilder.HasKey(AU => AU.Id);
		modelBuilder.Property(U => U.DisplayName).IsRequired().HasMaxLength(80);
		modelBuilder.Property(U => U.UserName).IsRequired().HasMaxLength(80);

		modelBuilder.OwnsOne(U => U.Address, address =>
		{
			address.WithOwner();

			address.Property(A => A.Street).IsRequired().HasMaxLength(40);
			address.Property(A => A.City).IsRequired().HasMaxLength(50);
			address.Property(A => A.State).IsRequired().HasMaxLength(80);
			address.Property(A => A.ZipCode).IsRequired().HasMaxLength(20);
		});

	}
}
