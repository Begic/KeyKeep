using System.Security.Cryptography;
using KeyKeep.Data.Contracts;
using KeyKeep.Data.Entities;
using KeyKeep.Data.Models;
using KeyKeep.Data.Services;
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

        using (var aes = new AesCryptoServiceProvider())
        {
            aes.KeySize = 256;
            var cryptKey = aes.Key;

            await db.Users.AddAsync(new User
            {
                FirstName = data.FirstName,
                LastName = data.LastName,
                Email = EditData.Encrypt(data.Email, cryptKey),
                LoginPassword = EditData.Encrypt(data.LoginPassword, cryptKey),
                CryptKeys = new List<CryptKey>
                {
                    new()
                    {
                        KeyValue = cryptKey
                    }
                }
            });
        }

        await db.SaveChangesAsync();
    }
}