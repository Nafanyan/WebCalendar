namespace Infrastructure.ConfigurationUtils.DataBase
{
    public interface IDBConfiguration
    {
        string GetConnectionString(string nameDB);
    }
}
