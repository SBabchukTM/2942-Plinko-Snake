namespace Runtime.Core.Infrastructure.Logger
{
    public interface IDebugger
    {
        void Log(string message);
        void Warning(string message);
        void Error(string message);
    }
}