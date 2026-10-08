namespace AutoService.Billing.Pricing;

public class PriceList : IPriceList
{
    private readonly Dictionary<string, decimal> _prices = new();

    public void SetPrice(string service, decimal price)
    {
        if (string.IsNullOrWhiteSpace(service))
            throw new ArgumentException("Название услуги не задано", nameof(service));
        if (price <= 0)
            throw new ArgumentOutOfRangeException(nameof(price), "Цена должна быть больше нуля");
        _prices[service] = price;
    }

    public decimal GetPrice(string service)
    {
        if (!_prices.TryGetValue(service, out var price))
            throw new KeyNotFoundException($"Нет такой услуги: {service}");
        return price;
    }
}
