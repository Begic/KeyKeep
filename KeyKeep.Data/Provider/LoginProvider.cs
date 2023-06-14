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

    public async Task<LoginInfo?> CheckUserForLogin(LoginInfo loginModel)
    {
        await using var db = await factory.CreateDbContextAsync().ConfigureAwait(false);

        return new LoginInfo();

        //return await db.Users.Where(x => x.Email.ToLower() == loginModel.Email.ToLower() && x.LoginPassword == loginModel.Password)
        //    .Select(x => new LoginInfo
        //    {
        //        Id =x.Id,
        //        Email = x.Email,
        //        Password = x.LoginPassword
        //    }).FirstOrDefaultAsync().ConfigureAwait(false);
    }
}