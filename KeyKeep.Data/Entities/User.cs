using System.ComponentModel.DataAnnotations;

namespace KeyKeep.Data.Entities;

public class User
{
    public int Id { get; set; }
    [MaxLength(200)] public string FirstName { get; set; }
    [MaxLength(200)] public string LastName { get; set; }
    [MaxLength(200)] public string Email { get; set; }
    [MaxLength(200)] public string LoginPassword { get; set; }

    public List<Password> Passwords { get; set; }
}