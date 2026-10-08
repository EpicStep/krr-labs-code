using AutoService.Billing.Domain;
using AutoService.Billing.Pricing;
using AutoService.Billing.Stock;

namespace AutoService.Billing;

public class OrderCalculator
{
    private readonly IPriceList _prices;
    private readonly IPartsStock _stock;

    public OrderCalculator(IPriceList prices, IPartsStock stock)
    {
        _prices = prices;
        _stock = stock;
    }

    public decimal Calculate(Order order, string clientType)
    {
        if (order == null)
            throw new ArgumentNullException(nameof(order));
        if (order.Services.Count == 0)
            throw new ArgumentException("В заказе нет услуг");

        decimal total = 0;
        foreach (var s in order.Services)
            total += _prices.GetPrice(s);

        foreach (var p in order.Parts)
        {
            if (p.Quantity <= 0)
                throw new ArgumentException("Неверное количество");
            if (!_stock.TryReserve(p.Article, p.Quantity))
                throw new InvalidOperationException("Недостаточно на складе: " + p.Article);
            total += _stock.GetPrice(p.Article) * p.Quantity;
        }

        if (clientType == "regular")
            total = total * 0.95m;
        else if (clientType == "vip")
            total = total * 0.9m;
        else if (clientType != "new")
            throw new ArgumentException("Неизвестный тип клиента");

        if (DateTime.Now.DayOfWeek == DayOfWeek.Sunday)
            total = total * 1.1m;

        total = Math.Round(total, 2);
        Console.WriteLine("Заказ " + order.Id + " рассчитан: " + total);
        return total;
    }
}
