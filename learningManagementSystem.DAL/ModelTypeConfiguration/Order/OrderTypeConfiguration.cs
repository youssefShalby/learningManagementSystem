



namespace learningManagementSystem.DAL.ModelTypeConfiguration;

public class OrderTypeConfiguration : IEntityTypeConfiguration<Order>
{
	public void Configure(EntityTypeBuilder<Order> modelBuilder)
	{
		modelBuilder.HasKey(O => O.Id);
		modelBuilder.Property(O => O.Price).IsRequired().HasColumnType("decimal(18, 2)");
	}
}
