namespace AutoService.Billing.Stock;

public interface IPartsStock
{
    decimal GetPrice(string article);
    bool TryReserve(string article, int quantity);
}
