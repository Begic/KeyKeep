using KeyKeep.Data.Models;

namespace KeyKeep.Data.Contracts;

public interface IDataProvider
{
    Task<List<PasswordInfo>> GetPasswordsFromUser(int userId);
    Task ArchivePassword(PasswordInfo item);
    Task EditPassword(PasswordInfo passwordInfoToEdit);
    Task<PasswordInfo?> GetPasswordInfo(int? passwordId);
    Task DeletePassword(PasswordInfo item);
}