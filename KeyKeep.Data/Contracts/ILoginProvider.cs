using KeyKeep.Data.Models;

namespace KeyKeep.Data.Contracts;

public interface ILoginProvider
{
    Task<LoginInfo?> CheckUserForLogin(string email, string password);
}