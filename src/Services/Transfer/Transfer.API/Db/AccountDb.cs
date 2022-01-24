using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Threading.Tasks;
using Transfer.API.Entities;

namespace Transfer.API.Db
{
    public class AccountDb
    {
        public async Task<StatusTransfer> ObterStatus(Guid guid)
        {
            IMongoClient client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("TransferDb");
            var collection = database.GetCollection<StatusTransfer>("Transfers");
            var status = await  collection.AsQueryable().Where(_ => _.TransferId == guid).FirstOrDefaultAsync();
            return status;
        }
    }
}
