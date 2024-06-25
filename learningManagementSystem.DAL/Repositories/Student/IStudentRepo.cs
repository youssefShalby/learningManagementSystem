

namespace learningManagementSystem.DAL.Repositories;

public interface IStudentRepo : IGenericRepo<Student>	
{
	Task<Student> GetByRefIdAsync(string refId);
}
