
namespace learningManagementSystem.DAL.Repositories;

public interface ILessonRepo : IGenericRepo<Lesson>
{
	Task<Lesson> GetByIdWithIncludesAsync(Guid id);
}
