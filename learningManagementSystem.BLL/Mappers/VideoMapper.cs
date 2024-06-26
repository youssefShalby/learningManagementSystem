

namespace learningManagementSystem.BLL.Mappers;

public class VideoMapper
{
	public static GetVideoForLessonDto ToGetForLessonDto(Video video)
	{
		return new GetVideoForLessonDto
		{
			Length = video.Length,
			Title = video.Title,
			UploadDate = video.UploadDate,
			Url = video.Url,
		};
	}

	public static Video ToVideoModel(UploadVideoDto video)
	{
		return new Video
		{
			Length = video.Length,
			Title = video.Title,
			UploadDate = video.UploadDate,
			Url = video.Url,
			Description = video.Description,
			Id = Guid.NewGuid(),
			LessonId = video.LessonId,
			FileFormat = video.FileFormat,
		};
	}
	
	
	public static GetVideoByIdWithIncludesDto ToGetWithIncludes(Video video)
	{
		return new GetVideoByIdWithIncludesDto
		{
			Length = video.Length,
			Title = video.Title,
			UploadDate = video.UploadDate,
			Url = video.Url,
			Description = video.Description,
			FileFormat = video.FileFormat,
			Comments = video.Comments?.Select(com => CommentMapper.ToGetForVideo(com)).ToList()
		};
	}
}
