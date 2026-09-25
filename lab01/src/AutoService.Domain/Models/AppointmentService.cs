namespace AutoService.Domain.Models;

public class AppointmentService
{
    public int Id { get; set; }
    public int AppointmentId { get; set; }
    public int ServiceId { get; set; }

    // Цена фиксируется в момент записи, чтобы прайс потом не менял историю
    public decimal PriceAtMoment { get; set; }

    public Appointment Appointment { get; set; } = null!;
    public Service Service { get; set; } = null!;
}
