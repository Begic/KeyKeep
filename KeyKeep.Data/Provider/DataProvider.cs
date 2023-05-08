using KeyKeep.Data.Contracts;
using Microsoft.EntityFrameworkCore;

namespace KeyKeep.Data.Provider;

public class DataProvider : IDataProvider
{
    private readonly IDbContextFactory<DataBaseContext> factory;

    public DataProvider(IDbContextFactory<DataBaseContext> factory)
    {
        this.factory = factory;
    }

    public async Task GetUseres()
    {
        await using var db = await factory.CreateDbContextAsync().ConfigureAwait(false);

    }
}