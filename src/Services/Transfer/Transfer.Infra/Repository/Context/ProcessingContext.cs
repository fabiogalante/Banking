using MongoDB.Driver;
using Processing.Domain.Entities;
using Processing.Domain.Repositories.Context;
using Processing.Infra.Settings;

namespace Processing.Infra.Repository.Context
{
    public class ProcessingContext : IProcessingContext
    {
        public ProcessingContext(IProcessingDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            TransferCollection = database.GetCollection<ProcessingAccount>(settings.CollectionName);
        }

      
        public IMongoCollection<ProcessingAccount> TransferCollection { get; }
    }
}
