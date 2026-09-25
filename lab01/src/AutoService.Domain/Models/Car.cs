namespace AutoService.Domain.Models;

public class Car
{
    public int Id { get; set; }
    public int ClientId { get; set; }
    public int ModelId { get; set; }
    public int Year { get; set; }
    public string LicensePlate { get; set; } = "";
    public string? Vin { get; set; }

    public Client Client { get; set; } = null!;
    public CarModel Model { get; set; } = null!;
    public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
}
