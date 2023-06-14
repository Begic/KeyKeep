namespace KeyKeep.Data.Entities;

public class Key
{
    public int Id { get; set; }

    public Password Password { get; set; }
    public int? PasswordId { get; set; }
    
    public User User { get; set; }
    public int? UserId { get; set; }
}