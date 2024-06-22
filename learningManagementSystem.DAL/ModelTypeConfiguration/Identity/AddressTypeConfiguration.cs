

namespace learningManagementSystem.DAL.ModelTypeConfiguration;

public class AddressTypeConfiguration : IEntityTypeConfiguration<Address>
{
	public void Configure(EntityTypeBuilder<Address> modelBuilder)
	{
		//> configure the properties
		modelBuilder.HasKey(A => A.Id);
		modelBuilder.Property(A => A.Street).IsRequired().HasMaxLength(40);
		modelBuilder.Property(A => A.City).IsRequired().HasMaxLength(50);
		modelBuilder.Property(A => A.State).IsRequired().HasMaxLength(80);
		modelBuilder.Property(A => A.ZipCode).IsRequired().HasMaxLength(20);
	}
}
