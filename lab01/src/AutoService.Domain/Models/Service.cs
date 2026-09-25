namespace AutoService.Domain.Models;

public class Service
{
    public int Id { get; set; }
    public int CategoryId { get; set; }
    public string Name { get; set; } = "";
    public decimal Price { get; set; }
    public int DurationMinutes { get; set; }

    public ServiceCategory Category { get; set; } = null!;
    public ICollection<AppointmentService> Appointments { get; set; } = new List<AppointmentService>();
}
