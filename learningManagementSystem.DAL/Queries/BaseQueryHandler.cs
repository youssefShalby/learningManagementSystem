

namespace learningManagementSystem.DAL.Queries;

public class BaseQueryHandler
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; } = 10;
    public bool IsDescending { get; set; }
    public string SortBy { get; set; } = string.Empty;
}
