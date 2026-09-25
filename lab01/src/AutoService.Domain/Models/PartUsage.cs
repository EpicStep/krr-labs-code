namespace AutoService.Domain.Models;

public class PartUsage
{
    public int Id { get; set; }
    public int AppointmentId { get; set; }
    public int PartId { get; set; }
    public int Quantity { get; set; }

    // Цена фиксируется на момент выдачи со склада
    public decimal PriceAtMoment { get; set; }

    public Appointment Appointment { get; set; } = null!;
    public Part Part { get; set; } = null!;
}
