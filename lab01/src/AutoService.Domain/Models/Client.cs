namespace AutoService.Domain.Models;

public class Client
{
    public int Id { get; set; }
    public string FullName { get; set; } = "";
    public string Phone { get; set; } = "";
    public string? Email { get; set; }

    public ICollection<Car> Cars { get; set; } = new List<Car>();
    public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
}
