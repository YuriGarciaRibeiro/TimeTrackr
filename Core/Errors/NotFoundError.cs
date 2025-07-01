using FluentResults;

namespace Core.Errors;

public class NotFoundError : Error
{
    public NotFoundError(string message)
        : base(message)
    {
        Metadata.Add("StatusCode", 404);
        Metadata.Add("Title", "Not Found");
    }
}