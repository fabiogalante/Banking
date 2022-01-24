namespace Processing.Infra.Settings
{
    public interface IProcessingDatabaseSettings
    {
        string CollectionName { get; }
        string ConnectionString { get; }
        string DatabaseName { get; }
    }
}
