

using StackExchange.Redis;

namespace learningManagementSystem.BLL.Service;

public class RedisService : IRedisService
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IDatabase _cacheDb;

	public RedisService(IUnitOfWork unitOfWork)
    {
		_unitOfWork = unitOfWork;
		try
		{
			var redis = ConnectionMultiplexer.Connect(_unitOfWork.Configuration.GetConnectionString("Redis"));
			_cacheDb = redis.GetDatabase();
		}
		catch
		{
			throw new Exception("the server of redis is down...!!");
		}
	}

	public async Task<bool> DeleteDataAsync<T>(string key)
	{
		var isExist = await _cacheDb.KeyExistsAsync(key);

		if (isExist)
		{
			return await _cacheDb.KeyDeleteAsync(key);
		}

		return false;
	}

	public async Task<T> GetDataAsync<T>(string key)
	{
		var value = await _cacheDb.StringGetAsync(key);
		if (!string.IsNullOrEmpty(value))
		{
			return JsonSerializer.Deserialize<T>(value!)!;
		}
		return default!;
	}

	public async Task<bool> SetDataAsync<T>(string key, T value, DateTimeOffset expireTime)
	{
		var expirationTime = expireTime.UtcDateTime.Subtract(DateTimeOffset.UtcNow.UtcDateTime);
		return await _cacheDb.StringSetAsync(key, JsonSerializer.Serialize(value), expirationTime);
	}
}
