using AutoService.Billing.Domain;
using AutoService.Billing.Infrastructure;
using AutoService.Billing.Pricing;
using AutoService.Billing.Stock;

namespace AutoService.Billing;

public class OrderCalculator
{
    // Наценка за работу в воскресенье
    public const decimal SundaySurcharge = 1.1m;

    private readonly IPriceList _prices;
    private readonly IPartsStock _stock;
    private readonly DiscountPolicy _discounts;
    private readonly IClock _clock;
    private readonly IOrderLog _log;

    public OrderCalculator(IPriceList prices, IPartsStock stock, DiscountPolicy discounts,
        IClock clock, IOrderLog log)
    {
        _prices = prices;
        _stock = stock;
        _discounts = discounts;
        _clock = clock;
        _log = log;
    }

    public decimal Calculate(Order order, ClientType clientType)
    {
        ArgumentNullException.ThrowIfNull(order);
        if (order.Services.Count == 0)
            throw new ArgumentException("В заказе нет услуг", nameof(order));

        decimal total = 0;
        foreach (var service in order.Services)
            total += _prices.GetPrice(service);

        foreach (var part in order.Parts)
            total += ReservePart(part);

        total = _discounts.Apply(total, clientType);

        if (_clock.Today.DayOfWeek == DayOfWeek.Sunday)
            total *= SundaySurcharge;

        total = Math.Round(total, 2);
        _log.Write($"Заказ {order.Id} рассчитан: {total}");
        return total;
    }

    private decimal ReservePart(PartLine part)
    {
        if (part.Quantity <= 0)
            throw new ArgumentException($"Неверное количество запчасти {part.Article}: {part.Quantity}");
        if (!_stock.TryReserve(part.Article, part.Quantity))
            throw new InvalidOperationException($"Недостаточно на складе: {part.Article}");
        return _stock.GetPrice(part.Article) * part.Quantity;
    }
}
