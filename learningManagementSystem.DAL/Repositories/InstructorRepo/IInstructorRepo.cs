

namespace learningManagementSystem.DAL.Repositories;

public interface IInstructorRepo : IGenericRepo<Instructor>
{
	Task<Instructor> GetByRefIdAsync(string refId);
}
