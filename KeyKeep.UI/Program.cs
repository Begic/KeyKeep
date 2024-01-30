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
            aes.Mode = CipherMode.CBC;
            aes.Key = new byte[32];

            await db.Users.AddAsync(new User
            {
                FirstName = "Admin",
                LastName = "User",
                Email = EditData.Encrypt("admin@user.at", new byte[32]),
                LoginPassword = EditData.Encrypt("admin123", new byte[32]),
                Passwords = new List<Password>
                {
                    new()
                    {
                        Title = "Für KeeyKep",
                        URL = EditData.Encrypt("https://localhost:44349/", aes.Key),
                        UserName = EditData.Encrypt("admin@user.at",aes.Key),
                        UserPassword = EditData.Encrypt("admin123", aes.Key),
                        CryptKeys = new List<CryptKey>
                        {
                            new()
                            {
                                KeyValue = aes.Key
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