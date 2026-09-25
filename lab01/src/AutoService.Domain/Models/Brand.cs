namespace AutoService.Domain.Models;

public class Brand
{
    public int Id { get; set; }
    public string Name { get; set; } = "";

    public ICollection<CarModel> Models { get; set; } = new List<CarModel>();
}
