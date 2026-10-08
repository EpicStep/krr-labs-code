using AutoService.Billing;

Console.OutputEncoding = System.Text.Encoding.UTF8;

var calc = new OrderCalculator();
var order = new Order
{
    Id = 1,
    Services = { "Замена масла", "Диагностика" },
    Parts = { new PartLine { Article = "OIL-5W30", Quantity = 4 }, new PartLine { Article = "FLT-OIL", Quantity = 1 } }
};
calc.Calculate(order, "regular");
