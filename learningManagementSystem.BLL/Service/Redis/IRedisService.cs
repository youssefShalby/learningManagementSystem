

namespace learningManagementSystem.BLL.Service;

public interface IRedisService
{
	Task<T> GetDataAsync<T>(string key);
	Task<bool> SetDataAsync<T>(string key,  T value, DateTimeOffset expireTime);
	Task<bool> DeleteDataAsync<T>(string key);
}
