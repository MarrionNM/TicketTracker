using System.Net;

namespace TaskTracker.API.Helpers.Exceptions;

public class ConflictException(string message, IEnumerable<string> errors) : BaseException(message, errors)
{
    public override HttpStatusCode StatusCode => HttpStatusCode.Conflict;
}