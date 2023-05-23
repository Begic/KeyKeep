using KeyKeep.Data.Contracts;
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