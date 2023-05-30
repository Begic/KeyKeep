using KeyKeep.Data.Models;

namespace KeyKeep.Data.Contracts;

public interface IDataProvider
{
    Task<List<PasswordInfo>> GetPasswordsFromUser(int userId);
    Task DeletePassword(PasswordInfo item);
    Task EditPassword(PasswordInfo passwordInfoToEdit);
    Task<PasswordInfo?> GetPasswordInfo(int? passwordId);
}