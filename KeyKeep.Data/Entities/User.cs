using System.ComponentModel.DataAnnotations;

namespace KeyKeep.Data.Entities;

public class User
{
    public int Id { get; set; }
    [MaxLength(200)] public string FirstName { get; set; }
    [MaxLength(200)] public string LastName { get; set; }
    [MaxLength(200)] public byte[] Email { get; set; }
    [MaxLength(200)] public byte[] LoginPassword { get; set; }

    public List<Password> Passwords { get; set; } = new();
    public List<Key> Keys { get; set; } = new();
}