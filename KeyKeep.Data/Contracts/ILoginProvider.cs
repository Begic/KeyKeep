namespace KeyKeep.Data.Contracts;

public interface ILoginProvider
{
    Task<string?> CheckUserForLogin(string email, string password);
}