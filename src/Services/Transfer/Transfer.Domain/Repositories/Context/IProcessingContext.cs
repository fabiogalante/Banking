using MongoDB.Driver;
using Processing.Domain.Entities;

namespace Processing.Domain.Repositories.Context
{
    public interface IProcessingContext
    {
        IMongoCollection<ProcessingAccount> TransferCollection { get; }
    }
}
