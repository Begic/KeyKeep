using KeyKeep.Data;
using KeyKeep.Data.Contracts;
using KeyKeep.Data.Provider;
using Microsoft.EntityFrameworkCore;
using MudBlazor.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddMudServices();


builder.Services.AddDbContextFactory<DataBaseContext>(options =>
    options.UseSqlite(@"DataSource=MyDataBase.db;"));

builder.Services.AddTransient<IDataProvider, DataProvider>();
builder.Services.AddTransient<ILoginProvider, LoginProvider>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
using (var db = scope.ServiceProvider.GetService<IDbContextFactory<DataBaseContext>>().CreateDbContext())
{
    db.Database.Migrate();
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