
namespace learningManagementSystem.API.ApiHelper;

public class CacheHelper : ICacheHelper
{
	private readonly IRedisService _redisService;
	private readonly IConfiguration _configuration;
    public CacheHelper(IRedisService redisService, IConfiguration configuration)
    {
        _redisService = redisService;
		_configuration = configuration;
    }

    public async Task<T> GetDataFromCache<T>(string cacheKey)
	{
		var data = await _redisService.GetDataAsync<T>(cacheKey);
		if(data is null)
		{
			return default!;
		}
		return data;
	}

	public async Task SetDataInCache<T>(string key, T data)
	{
		var expireTime = DateTimeOffset.Now.AddDays(int.Parse(_configuration["RedisCache:endpointCacheTimePerDay"] ?? "1"));
		await _redisService.SetDataAsync(key, data, expireTime);
	}

	public async Task SetDataInShortTimeCache<T>(string key, T data)
	{
		var expireTime = DateTimeOffset.Now.AddMinutes(int.Parse(_configuration["RedisCache:shortTimeCachingByMin"] ?? "5"));
		await _redisService.SetDataAsync(key, data, expireTime);
	}
}
