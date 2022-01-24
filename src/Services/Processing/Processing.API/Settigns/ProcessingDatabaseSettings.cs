using Processing.Infra.Settings;

namespace Processing.API.Settigns
{
    public class ProcessingDatabaseSettings : IProcessingDatabaseSettings
    {
        public string CollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
