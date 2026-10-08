namespace Interfaces.Task4;

public interface IPayable
{
    void Pay(decimal amount);
}

public class CreditCard : IPayable
{
    private readonly string _number;

    public CreditCard(string number) => _number = number;

    public void Pay(decimal amount) =>
        Console.WriteLine($"Оплата картой *{_number[^4..]} на сумму {amount:F2} руб.");
}

public class Cash : IPayable
{
    public void Pay(decimal amount) => Console.WriteLine($"Оплата наличными: {amount:F2} руб.");
}

public static class Checkout
{
    public static void ProcessPayment(IPayable method, decimal amount)
    {
        if (amount <= 0)
        {
            Console.WriteLine("Сумма должна быть больше нуля");
            return;
        }
        method.Pay(amount);
    }
}
