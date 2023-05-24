using KeyKeep.Data;
using KeyKeep.Data.Contracts;
using KeyKeep.Data.Entities;
using KeyKeep.Data.Provider;
using KeyKeep.Data.Services;
using Microsoft.EntityFrameworkCore;
using MudBlazor.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddMudServices();

builder.Services.AddTransient<IDataProvider, DataProvider>();
builder.Services.AddTransient<ILoginProvider, LoginProvider>();
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
        await db.Users.AddAsync(new User
        {
            FirstName = "Admin",
            LastName = "User",
            Email = "admin@user.at",
            LoginPassword = "admin123"
        });

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