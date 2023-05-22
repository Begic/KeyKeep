using KeyKeep.Data.Models;

namespace KeyKeep.Data.Contracts;

public interface IDataProvider
{
    Task<List<PasswordInfo>> GetPasswordsFromUser(int userId);
}