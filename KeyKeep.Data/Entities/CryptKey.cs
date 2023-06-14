namespace KeyKeep.Data.Entities;

public class CryptKey
{
    public int Id { get; set; }

    public Password Password { get; set; }
    public int? PasswordId { get; set; }
    
    public User User { get; set; }
    public int? UserId { get; set; }

    public byte[] KeyValue { get; set; }
}