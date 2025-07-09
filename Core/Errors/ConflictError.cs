namespace Core.Errors;

public class ConflictError : Error
{
    public ConflictError(string message) : base(message)
    {
        Metadata.Add("StatusCode", 409);
        Metadata.Add("Title", "Conflict");
    }
}
