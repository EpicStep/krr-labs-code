using AutoService.Billing;
using AutoService.Billing.Domain;
using AutoService.Billing.Pricing;
using AutoService.Billing.Stock;

Console.OutputEncoding = System.Text.Encoding.UTF8;

var prices = new PriceList();
prices.SetPrice("Замена масла", 1200m);
prices.SetPrice("Диагностика", 900m);
prices.SetPrice("Замена колодок", 2500m);

var warehouse = new PartsWarehouse(new()
{
    ["OIL-5W30"] = (650m, 20),
    ["FLT-OIL"] = (450m, 10),
    ["BRK-PAD"] = (2300m, 4),
});

var calc = new OrderCalculator(prices, warehouse, new DiscountPolicy());
var order = new Order
{
    Id = 1,
    Services = { "Замена масла", "Диагностика" },
    Parts = { new PartLine { Article = "OIL-5W30", Quantity = 4 }, new PartLine { Article = "FLT-OIL", Quantity = 1 } }
};
calc.Calculate(order, ClientType.Regular);
