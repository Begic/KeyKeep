using KeyKeep.Data.Contracts;
using KeyKeep.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace KeyKeep.Data.Provider;

public class DataProvider : IDataProvider
{
    private readonly IDbContextFactory<DataBaseContext> factory;

    public DataProvider(IDbContextFactory<DataBaseContext> factory)
    {
        this.factory = factory;
    }

    public async Task AddUser()
    {
        await using var db = await factory.CreateDbContextAsync().ConfigureAwait(false);

        var ka = db.Users;

        await db.Users.AddAsync(new User
        {
            FirstName = "Admin",
            LastName = "User",
            Email = "admin@user.at",
            LoginPassword = "admin123"
        });

        await db.SaveChangesAsync();
    }

    public async Task GetPasswords()
    {
        await using var db = await factory.CreateDbContextAsync().ConfigureAwait(false);
    }

    public async Task GetUser()
    {
        await using var db = await factory.CreateDbContextAsync().ConfigureAwait(false);

        var user = await db.Users.ToListAsync();

    }
}