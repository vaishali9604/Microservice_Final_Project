using System;

namespace MT.OnlineRestaurant.LoggingManagement
{
    public interface ILogService
    {
        void LogException(Exception exception);
        void LogMessage(string message);
    }
}
