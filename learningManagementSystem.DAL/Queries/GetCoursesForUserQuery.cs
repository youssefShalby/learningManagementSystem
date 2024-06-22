

namespace learningManagementSystem.DAL.Queries;

public class GetCoursesForUserQuery
{
    public string email { get; set; } = string.Empty;
    public int PageNumber { get; set; }

}
