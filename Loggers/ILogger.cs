namespace Loggers
{
    public interface ILogger
    {
        bool Log(DateTime logDateTime, string message, object source);
    }
}