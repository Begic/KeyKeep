namespace KeyKeep.Data.Contracts;

public interface IDataProvider
{
    Task<bool> CheckUserForLogin(string email, string password);
}