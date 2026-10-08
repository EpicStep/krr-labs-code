namespace AutoService.Data.Entities;

public class Client
{
    public int Id { get; set; }
    public string FullName { get; set; } = "";
    public string Phone { get; set; } = "";
    public string? Email { get; set; }

    public List<Car> Cars { get; set; } = new();
}
