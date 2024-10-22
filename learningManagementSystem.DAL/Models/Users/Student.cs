﻿

namespace learningManagementSystem.DAL.Models;

public class Student : BaseModel<Guid>
{

	[ForeignKey(nameof(AppUser))]
	public string? UserRefId { get; set; }
	public ApplicationUser? AppUser { get; set; }
    public ICollection<StudentCourse>? StudentCourses { get; set; }

}
