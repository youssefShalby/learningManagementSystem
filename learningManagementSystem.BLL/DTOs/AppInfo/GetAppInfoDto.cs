

namespace learningManagementSystem.BLL.DTOs;

public class GetAppInfoDto
{
    public int CorsesCountOfLastMonth { get; set; }
    public int CorsesCountOfLastYear { get; set; }
    public int OrdersCountOfLastMonth { get; set; }
    public int OrdersCountOfLastYear { get; set; }
    public int AllCategoriesCount { get; set; }
    public int AllCoursesCount { get; set; }
    public int AllOrdersCount { get; set; }
    public int BlockedUserCount { get; set; }
    public int UnBlockedUserCount { get; set; }
    public int AppUsersCount { get; set; }
    public int InstructorsCount { get; set; }
    public int StudentCount { get; set; }
    public int AdminsCount { get; set; }
}
