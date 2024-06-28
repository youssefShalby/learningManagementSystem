namespace learningManagementSystem.API.Errors;

public class ApiResponse
{
    public int StatusCode { get; set; }
    public string? Message { get; set; }
    public ApiResponse(int statusCode, string message = null!)
    {
        StatusCode = statusCode;
        Message = message;
    }

    private string GetMessageFromStatusCode(int statusCode)
    {
        return statusCode switch
        {
			400 => "you made a bad request..!!",
			401 => "you're not authorized..!!",
			404 => "Resource not found..!!",
			500 => "there an error in our servers and we fix it now, please try to make this requet later..!!",
			_ => null!
		};
    }
}
