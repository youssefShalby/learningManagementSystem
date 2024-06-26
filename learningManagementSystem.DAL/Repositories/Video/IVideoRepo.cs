

namespace learningManagementSystem.DAL.Repositories;

public interface IVideoRepo : IGenericRepo<Video>
{
	Task<Video> GetByIdWithIncludesAsync(Guid id);
}
