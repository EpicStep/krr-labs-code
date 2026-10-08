namespace UsersApi.Data;

public class User
{
    public int Id { get; set; }
    public string Login { get; set; } = "";
    public string PassHash { get; set; } = "";
}
