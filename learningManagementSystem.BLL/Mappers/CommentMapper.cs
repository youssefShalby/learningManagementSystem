

namespace learningManagementSystem.BLL;

public class CommentMapper
{
	public static GetCommentsForVideoDto ToGetForVideo(Comment comment)
	{
		return new GetCommentsForVideoDto
		{
			Content = comment.Content,
			CreatedAt = comment.CreatedAt,
			UserName = comment.AppUser?.DisplayName ?? "NA",
			UserId = comment.UserId,


			/*Replies = comment.Replies?.Select(relpy => new GetRepliesDto
			{
				UserId = relpy.UserId,
				Content = relpy.Content,
				CreatedAt = relpy.CreatedAt,
				UserName  = relpy.AppUser?.DisplayName ?? "NA"

			}).ToList(),*/
		};
	}

	public static GetCommentForCourseDto ToGetForCourse(Comment comment)
	{
		return new GetCommentForCourseDto
		{
			Content = comment.Content,
			CreatedAt = comment.CreatedAt,
			UserName = comment.AppUser?.DisplayName ?? "NA",
			UserId = comment.UserId,


			/*
			Replies = comment.Replies?.Select(relpy => new GetRepliesDto
			{
				UserId = relpy.UserId,
				Content = relpy.Content,
				CreatedAt = relpy.CreatedAt,
				UserName = relpy.AppUser?.DisplayName ?? "NA"

			}).ToList(),*/
		};
	}
}
