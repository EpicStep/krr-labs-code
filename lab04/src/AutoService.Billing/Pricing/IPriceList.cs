namespace AutoService.Billing.Pricing;

public interface IPriceList
{
    decimal GetPrice(string service);
}
