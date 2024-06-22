

namespace learningManagementSystem.DAL.Models;

public class Address : BaseModel<Guid>
{
    public string State { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string Street { get; set; } = string.Empty;
    public string ZipCode { get; set; } = string.Empty;

    [ForeignKey(nameof(AppUser))]
    public string? AppUserId { get; set; }
    public ApplicationUser? AppUser { get; set; }
}
