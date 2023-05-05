using System.ComponentModel.DataAnnotations;

namespace KeyKeep.Data.Entities;

public class User
{
    public int Id { get; set; }
    [MaxLength(200)] public string Firstname { get; set; }
    [MaxLength(200)] public string LastName { get; set; }
    [MaxLength(200)] public string Email { get; set; }
    [MaxLength(200)] public string LoginPasswort { get; set; }

    public List<Passwort> Passworts { get; set; }
}