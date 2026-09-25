namespace AutoService.Domain.Models;

public class CarModel
{
    public int Id { get; set; }
    public int BrandId { get; set; }
    public string Name { get; set; } = "";

    public Brand Brand { get; set; } = null!;
    public ICollection<Car> Cars { get; set; } = new List<Car>();
}
