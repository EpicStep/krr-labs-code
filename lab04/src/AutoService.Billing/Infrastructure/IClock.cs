namespace AutoService.Billing.Infrastructure;

public interface IClock
{
    DateTime Today { get; }
}

public class SystemClock : IClock
{
    public DateTime Today => DateTime.Today;
}
