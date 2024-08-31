using Assignment_PRN.Entities;

namespace Assignment_PRN.Models;

public class UserList
{
    public string? Id { get; init; }
    public string UserName { get; set; } = null!;
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public bool LockoutEnabled { get; set; }
    public int totalComment { get; set; }
}