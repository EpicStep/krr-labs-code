namespace AutoService.Billing.Infrastructure;

public interface IOrderLog
{
    void Write(string message);
}

public class ConsoleOrderLog : IOrderLog
{
    public void Write(string message) => Console.WriteLine(message);
}
