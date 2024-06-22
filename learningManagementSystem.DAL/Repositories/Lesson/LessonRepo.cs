

namespace learningManagementSystem.DAL.Repositories;

public class LessonRepo : GenericRepo<Lesson>, ILessonRepo
{
    public LessonRepo(AppDbContext context, IConfiguration configuration) : base(context, configuration)
    {
        
    }
}
