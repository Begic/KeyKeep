using KeyKeep.Data.Contracts;
using KeyKeep.Data.Entities;
using KeyKeep.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace KeyKeep.Data.Provider;

public class RegisterProvider : IRegisterProvider
{
    private readonly IDbContextFactory<DataBaseContext> factory;

    public RegisterProvider(IDbContextFactory<DataBaseContext> factory)
    {
        this.factory = factory;
    }

    public async Task AddUser(RegisterInfo data)
    {
        await using var db = await factory.CreateDbContextAsync().ConfigureAwait(false);

        await db.Users.AddAsync(new User
        {
            FirstName = data.FirstName,
            LastName = data.LastName,
            Email = data.Email,
            LoginPassword = data.LoginPassword
        });

        await db.SaveChangesAsync();
    }
}