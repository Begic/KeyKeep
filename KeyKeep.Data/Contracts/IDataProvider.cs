namespace KeyKeep.Data.Contracts;

public interface IDataProvider
{
    Task GetPasswords();
    Task AddUser();
    Task GetUser();
}