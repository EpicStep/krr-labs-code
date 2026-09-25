namespace AutoService.Domain.Models;

public class Part
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public string Article { get; set; } = "";
    public decimal Price { get; set; }
    public int StockQty { get; set; }

    public ICollection<PartUsage> Usages { get; set; } = new List<PartUsage>();
}
