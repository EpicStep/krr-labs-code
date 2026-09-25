namespace AutoService.Domain.Models;

public class Employee
{
    public int Id { get; set; }
    public string FullName { get; set; } = "";
    public string Position { get; set; } = "";
    public string Phone { get; set; } = "";

    public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
}
