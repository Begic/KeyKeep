using KeyKeep.Data.Contracts;
using Microsoft.EntityFrameworkCore;

namespace KeyKeep.Data.Provider;

public class LoginProvider : ILoginProvider
{
    private readonly IDbContextFactory<DataBaseContext> factory;

    public LoginProvider(IDbContextFactory<DataBaseContext> factory)
    {
        this.factory = factory;
    }

    public async Task<bool> CheckUserForLogin(string email, string password)
    {
        await using var db = await factory.CreateDbContextAsync().ConfigureAwait(false);

        return db.Users.Any(x => x.Email == email && x.LoginPassword == password);
    }
}