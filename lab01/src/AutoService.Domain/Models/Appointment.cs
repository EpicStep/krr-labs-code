namespace AutoService.Domain.Models;

public class Appointment
{
    public int Id { get; set; }
    public int ClientId { get; set; }
    public int CarId { get; set; }
    public int EmployeeId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime PlannedAt { get; set; }
    public AppointmentStatus Status { get; set; }

    public Client Client { get; set; } = null!;
    public Car Car { get; set; } = null!;
    public Employee Employee { get; set; } = null!;
    public ICollection<AppointmentService> Services { get; set; } = new List<AppointmentService>();
    public ICollection<PartUsage> Parts { get; set; } = new List<PartUsage>();
    public Payment? Payment { get; set; }
}

public enum AppointmentStatus
{
    Created = 0,
    Confirmed = 1,
    InProgress = 2,
    Completed = 3,
    Cancelled = 4
}
