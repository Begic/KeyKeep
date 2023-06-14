using KeyKeep.Data.Contracts;
using KeyKeep.Data.Entities;
using KeyKeep.Data.Models;
using KeyKeep.Data.Services;
using Microsoft.EntityFrameworkCore;

namespace KeyKeep.Data.Provider;

public class DataProvider : IDataProvider
{
    private readonly IDbContextFactory<DataBaseContext> factory;
    private readonly UserService userService;

    public DataProvider(IDbContextFactory<DataBaseContext> factory, UserService userService)
    {
        this.factory = factory;
        this.userService = userService;
    }

    public async Task<List<PasswordInfo>> GetPasswordsFromUser(int userId)
    {
        await using var db = await factory.CreateDbContextAsync().ConfigureAwait(false);

        return await db.Passwords.Where(x => x.UserId == userId).Select(x => new PasswordInfo
        {
            Id = x.Id,
            Title = x.Title,
            //URL = x.URL,
            //Description = x.Description,
            //UserName = x.UserName,
            //UserPassword = x.UserPassword,
            IsDeleted = x.IsDeleted,
        }).ToListAsync().ConfigureAwait(false);
    }

    public async Task ArchivePassword(PasswordInfo item)
    {
        await using var db = await factory.CreateDbContextAsync().ConfigureAwait(false);

        var toArchiv = await db.Passwords.FirstOrDefaultAsync(x => x.Id == item.Id).ConfigureAwait(false);

        if (toArchiv != null)
        {
            toArchiv.IsDeleted = !toArchiv.IsDeleted;

            await db.SaveChangesAsync();
        }
    }

    public async Task EditPassword(PasswordInfo passwordInfoToEdit)
    {
        await using var db = await factory.CreateDbContextAsync().ConfigureAwait(false);

        var toEdit = await db.Passwords.FirstOrDefaultAsync(x => x.Id == passwordInfoToEdit.Id).ConfigureAwait(false);

        if (toEdit == null)
        {
            await db.Passwords.AddAsync(toEdit = new Password());

            toEdit.UserId = userService.CurrentUser.Id;
        }

        toEdit.Title = passwordInfoToEdit.Title;
        toEdit.Description = passwordInfoToEdit.Description;
        //toEdit.URL = passwordInfoToEdit.URL;

        //toEdit.UserName = passwordInfoToEdit.UserName;
        //toEdit.UserPassword = passwordInfoToEdit.UserPassword;

        toEdit.IsDeleted = passwordInfoToEdit.IsDeleted;

        await db.SaveChangesAsync();
    }

    public async Task<PasswordInfo?> GetPasswordInfo(int? passwordId)
    {
        await using var db = await factory.CreateDbContextAsync().ConfigureAwait(false);

        return await db.Passwords.Where(x => x.Id == passwordId).Select(x => new PasswordInfo
        {
            Id = x.Id,
            Description = x.Description,
            Title = x.Title,
            //URL = x.URL,
            //UserName = x.UserName,
            //IsDeleted = x.IsDeleted,
            //UserPassword = x.UserPassword,
        }).FirstOrDefaultAsync();
    }

    public async Task DeletePassword(PasswordInfo item)
    {
        await using var db = await factory.CreateDbContextAsync().ConfigureAwait(false);

        var toDelete = await db.Passwords.FirstOrDefaultAsync(x => x.Id == item.Id).ConfigureAwait(false);

        if (toDelete != null)
        {
            db.Passwords.Remove(toDelete);

            await db.SaveChangesAsync();
        }
    }
}