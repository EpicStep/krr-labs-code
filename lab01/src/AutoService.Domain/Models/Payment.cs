namespace AutoService.Domain.Models;

public class Payment
{
    public int Id { get; set; }
    public int AppointmentId { get; set; }
    public decimal Amount { get; set; }
    public string Method { get; set; } = "";
    public DateTime PaidAt { get; set; }

    public Appointment Appointment { get; set; } = null!;
}
