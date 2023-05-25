using KeyKeep.Data.Models;

namespace KeyKeep.Data.Contracts;

public interface IRegisterProvider
{
    Task AddUser(RegisterInfo registerInfoToEdit);
}