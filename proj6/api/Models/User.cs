public class User
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Username { get; set; }
    public string Role { get; set; } // Użytkownik lub Administrator
}
