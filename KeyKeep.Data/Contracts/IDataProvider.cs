using KeyKeep.Data.Models;

namespace KeyKeep.Data.Contracts;

public interface IDataProvider
{
    Task<bool> CheckUserForLogin(string email, string password);
    Task<List<PasswordInfo>> GetPasswordsFromUser(int userId);
}