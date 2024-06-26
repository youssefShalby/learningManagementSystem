

namespace learningManagementSystem.DAL.Repositories;

public class CourseAdvantegeRepo : GenericRepo<CourseAdvantage> , ICourseAdvantegeRepo
{
    public CourseAdvantegeRepo(AppDbContext context, IConfiguration configuration) : base(context, configuration)
    {
        
    }
}
