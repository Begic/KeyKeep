using KeyKeep.Data.Contracts;
using KeyKeep.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace KeyKeep.Data.Provider;

public class LoginProvider : ILoginProvider
{
    private readonly IDbContextFactory<DataBaseContext> factory;

    public LoginProvider(IDbContextFactory<DataBaseContext> factory)
    {
        this.factory = factory;
    }

    public async Task<LoginInfo?> CheckUserForLogin(string email, string password)
    {
        await using var db = await factory.CreateDbContextAsync().ConfigureAwait(false);

        return await db.Users.Where(x => x.Email.ToLower() == email.ToLower() && x.LoginPassword == password)
            .Select(x => new LoginInfo
            {
                Id =x.Id,
                Email = x.Email,
                Password = x.LoginPassword
            }).FirstOrDefaultAsync().ConfigureAwait(false);
    }
}