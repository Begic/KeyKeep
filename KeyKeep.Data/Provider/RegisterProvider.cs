using KeyKeep.Data.Contracts;
using Microsoft.EntityFrameworkCore;

namespace KeyKeep.Data.Provider;

public class RegisterProvider : IRegisterProvider
{
    private readonly IDbContextFactory<DataBaseContext> factory;

    public RegisterProvider(IDbContextFactory<DataBaseContext> factory)
    {
        this.factory = factory;
    }
}