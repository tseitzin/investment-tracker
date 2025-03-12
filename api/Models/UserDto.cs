namespace api.Models;

public class UserDto
{
    public int Id { get; set; }
    public string? Email { get; set; }
    public string? Name { get; set; }
    public bool IsAdmin { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime LastLoginDate { get; set; }
    public int NumberOfLogins { get; set; }
    public int FailedLogins { get; set; }
}