using System.Reflection;

namespace Booking.API.MethodTimer;

public static class MethodTimeLogger
{
    public static ILogger logger;

    public static void Log(MethodBase methodBase, TimeSpan timeSpan, string message)
    {
        logger.LogInformation("{Class}.{Method} executed in {Duration} ms. {Message}",
            methodBase.DeclaringType!.Name, methodBase.Name, timeSpan.TotalMilliseconds, message ?? "");
    }
}
