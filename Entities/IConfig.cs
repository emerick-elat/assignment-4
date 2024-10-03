namespace Entities
{
    public interface IConfig
    {
        decimal InitialBalance { get; set; }
        string Path { get; set; }
        string SystemAccountNumber { get; set; }
        string ConnectionString { get; set; }
    }
}