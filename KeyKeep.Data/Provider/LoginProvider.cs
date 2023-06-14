using KeyKeep.Data.Contracts;
using KeyKeep.Data.Models;
using KeyKeep.Data.Services;
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

        foreach (var user in await db.Users.ToListAsync())
        {
            if (EditData.Decrypt(user.Email, new byte[32]) == loginModel.Email && 
                EditData.Decrypt(user.LoginPassword, new byte[32]) == loginModel.Password)
            {
                return new LoginInfo
                {
                    Id = user.Id,
                    Email = EditData.Decrypt(user.Email, new byte[32]),
                    Password = EditData.Decrypt(user.LoginPassword, new byte[32])
                };
            }
        }

        return null;
    }
}