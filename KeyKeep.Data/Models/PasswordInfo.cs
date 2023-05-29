namespace KeyKeep.Data.Models;

public class PasswordInfo
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string URL { get; set; }
    public string Description { get; set; }
    public string UserName { get; set; }
    public string UserPassword { get; set; }
    public bool IsDeleted { get; set; }
}