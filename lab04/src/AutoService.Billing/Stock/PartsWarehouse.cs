namespace AutoService.Billing.Stock;

public class PartsWarehouse : IPartsStock
{
    private readonly Dictionary<string, (decimal Price, int Qty)> _stock;

    public PartsWarehouse(Dictionary<string, (decimal Price, int Qty)> stock)
    {
        _stock = stock;
    }

    public decimal GetPrice(string article)
    {
        if (!_stock.TryGetValue(article, out var item))
            throw new KeyNotFoundException($"Нет запчасти с артикулом {article}");
        return item.Price;
    }

    public bool TryReserve(string article, int quantity)
    {
        if (!_stock.TryGetValue(article, out var item))
            return false;
        if (item.Qty < quantity)
            return false;
        _stock[article] = (item.Price, item.Qty - quantity);
        return true;
    }

    public int GetQuantity(string article) => _stock.TryGetValue(article, out var item) ? item.Qty : 0;
}
