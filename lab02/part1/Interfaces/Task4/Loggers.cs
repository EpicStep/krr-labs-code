namespace Interfaces.Task4;

public interface ILogger
{
    void Log(string message);
}

public class ConsoleLogger : ILogger
{
    public void Log(string message) => Console.WriteLine($"[Console] {DateTime.Now:HH:mm:ss} {message}");
}

public class FileLogger : ILogger
{
    private readonly string _path;

    public FileLogger(string path) => _path = path;

    public void Log(string message) =>
        File.AppendAllText(_path, $"{DateTime.Now:HH:mm:ss} {message}{Environment.NewLine}");
}

public static class Worker
{
    public static void DoWork(ILogger logger)
    {
        logger.Log("Начало работы");
        for (int i = 1; i <= 3; i++)
            logger.Log($"Обработан шаг {i}");
        logger.Log("Работа завершена");
    }
}
