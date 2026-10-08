using AutoService.Billing.Domain;

namespace AutoService.Billing.Pricing;

public class DiscountPolicy
{
    public decimal Apply(decimal total, ClientType clientType)
    {
        switch (clientType)
        {
            case ClientType.New:
                return total;
            case ClientType.Regular:
                return total * 0.95m;
            case ClientType.Vip:
                return total * 0.90m;
            default:
                throw new ArgumentOutOfRangeException(nameof(clientType), "Неизвестный тип клиента");
        }
    }
}
