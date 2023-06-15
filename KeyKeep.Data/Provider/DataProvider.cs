using System.Security.Cryptography;
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

        var test = await db.Passwords.Include(x=> x.CryptKeys).FirstOrDefaultAsync(x => x.UserId == userId).ConfigureAwait(false);

        return await db.Passwords.Include(x=> x.CryptKeys).Where(x => x.UserId == userId).Select(x => new PasswordInfo
        {
            Id = x.Id,
            Title = x.Title,
            Description = x.Description,
            URL = EditData.Decrypt(x.URL, x.CryptKeys.First(y => y.Id == x.Id).KeyValue),
            UserName = EditData.Decrypt(x.URL, x.CryptKeys.First(y => y.Id == x.Id).KeyValue),
            UserPassword = EditData.Decrypt(x.URL, x.CryptKeys.First(y => y.Id == x.Id).KeyValue),
            IsDeleted = x.IsDeleted
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
        toEdit.IsDeleted = passwordInfoToEdit.IsDeleted;

        using (var aes = new AesCryptoServiceProvider())
        {
            aes.KeySize = 256;

            toEdit.URL = EditData.Encrypt(passwordInfoToEdit.URL, aes.Key);
            toEdit.UserName = EditData.Encrypt(passwordInfoToEdit.UserName, aes.Key);
            toEdit.UserPassword = EditData.Encrypt(passwordInfoToEdit.UserPassword, aes.Key);

            toEdit.CryptKeys = new List<CryptKey>
            {
                new()
                {
                    KeyValue = aes.Key
                }
            };
        }

        await db.SaveChangesAsync();
    }

    public async Task<PasswordInfo?> GetPasswordInfo(int? passwordId)
    {
        await using var db = await factory.CreateDbContextAsync().ConfigureAwait(false);

        return await db.Passwords.Include(x=> x.CryptKeys).Where(x => x.Id == passwordId).Select(x => new PasswordInfo
        {
            Id = x.Id,
            Description = x.Description,
            Title = x.Title,
            IsDeleted = x.IsDeleted,
            URL = EditData.Decrypt(x.URL, x.CryptKeys.FirstOrDefault(y => y.Id == x.Id).KeyValue),
            UserName = EditData.Decrypt(x.URL, x.CryptKeys.FirstOrDefault(y => y.Id == x.Id).KeyValue),
            UserPassword = EditData.Decrypt(x.URL, x.CryptKeys.FirstOrDefault(y => y.Id == x.Id).KeyValue),
            CryptKey = x.CryptKeys.FirstOrDefault(y => y.Id == x.Id).KeyValue
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