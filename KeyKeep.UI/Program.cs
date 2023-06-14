using System.Security.Cryptography;
using KeyKeep.Data;
using KeyKeep.Data.Contracts;
using KeyKeep.Data.Entities;
using KeyKeep.Data.Provider;
using KeyKeep.Data.Services;
using Microsoft.EntityFrameworkCore;
using MudBlazor;
using MudBlazor.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

builder.Services.AddMudServices(
    conf => conf.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.BottomRight);

builder.Services.AddTransient<IDataProvider, DataProvider>();
builder.Services.AddTransient<ILoginProvider, LoginProvider>();
builder.Services.AddTransient<IRegisterProvider, RegisterProvider>();
builder.Services.AddScoped<UserService>();

builder.Services.AddDbContextFactory<DataBaseContext>(options =>
    options.UseSqlite(@"DataSource=MyDataBase.db;"));


var app = builder.Build();

using (var scope = app.Services.CreateScope())
using (var db = scope.ServiceProvider.GetService<IDbContextFactory<DataBaseContext>>().CreateDbContext())
{
    db.Database.Migrate();

    if (!db.Users.Any())
    {
        using (var aes = new AesCryptoServiceProvider())
        {
            aes.KeySize = 256;
            var cryptKey = aes.Key;

            await db.Users.AddAsync(new User
            {
                FirstName = "Admin",
                LastName = "User",
                Email = EditData.Encrypt("admin@user.at", cryptKey),
                LoginPassword = EditData.Encrypt("admin123", cryptKey),
                CryptKeys = new List<CryptKey>
                {
                    new()
                    { 
                        KeyValue = cryptKey
                    }
                },
                Passwords = new List<Password>
                {
                    new()
                    {
                        Title = "Für KeeyKep",
                        UserName = EditData.Encrypt("admin@user.at",cryptKey) ,
                        UserPassword = EditData.Encrypt("admin123", cryptKey) ,
                        CryptKeys = new List<CryptKey>
                        {
                            new()
                            {
                                KeyValue = cryptKey
                            }
                        }
                    }
                }
            });
        }

        await db.SaveChangesAsync();
    }
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();