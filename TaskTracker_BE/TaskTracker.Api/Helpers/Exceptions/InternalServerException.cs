namespace TaskTracker.API.Helpers.Exceptions;

public class InternalServerException(string message, Exception inner = null) : Exception(message, inner)
{
}