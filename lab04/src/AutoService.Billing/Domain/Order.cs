namespace AutoService.Billing.Domain;

public class Order
{
    public int Id { get; set; }
    public List<string> Services { get; set; } = new();
    public List<PartLine> Parts { get; set; } = new();
}

public class PartLine
{
    public string Article { get; set; } = "";
    public int Quantity { get; set; }
}

public enum ClientType
{
    New,
    Regular,
    Vip
}
