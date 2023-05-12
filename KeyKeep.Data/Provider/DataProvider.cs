using KeyKeep.Data.Contracts;
using KeyKeep.Data.Entities;
using KeyKeep.Data.Models;
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

    public async Task<bool> CheckUserForLogin(string email, string password)
    {
        await using var db = await factory.CreateDbContextAsync().ConfigureAwait(false);

        return db.Users.Any(x => x.Email == email && x.LoginPassword == password);
    }

    public async Task<List<PasswordInfo>> GetPasswordsFromUser(int userId)
    {
        await using var db = await factory.CreateDbContextAsync().ConfigureAwait(false);

        return await db.Passwords.Where(x => x.UserId == userId).Select(x => new PasswordInfo
        {
            Id = x.Id,
            Title = x.Title,
            URL = x.URL,
            Description = x.Description,
            UserName = x.UserName,
            UserPassword = x.UserPassword
        }).ToListAsync().ConfigureAwait(false);
    }
}