namespace AutoService.Billing;

public class PartsWarehouse
{
    private readonly Dictionary<string, (decimal Price, int Qty)> _stock = new()
    {
        ["OIL-5W30"] = (650m, 20),
        ["FLT-OIL"] = (450m, 10),
        ["BRK-PAD"] = (2300m, 4),
    };

    public decimal GetPrice(string article)
    {
        return _stock[article].Price;
    }

    public bool Reserve(string article, int qty)
    {
        if (!_stock.ContainsKey(article))
            return false;
        var item = _stock[article];
        if (item.Qty < qty)
            return false;
        _stock[article] = (item.Price, item.Qty - qty);
        return true;
    }
}
