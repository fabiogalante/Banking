using System;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Processing.Domain.Entities;
using Processing.Domain.Repositories.Context;
using Processing.Domain.Repositories.CreateTransfer;

namespace Processing.Infra.Repository.CreateTransfer
{
    public class ProcessingRepository : IProcessingRepository
    {
        private readonly IProcessingContext _context;

        public ProcessingRepository(IProcessingContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task CreateTransfer(ProcessingAccount processingAccount)
        {
            await _context.TransferCollection.InsertOneAsync(processingAccount);
        }

        public async Task<ProcessingAccount> GetTransfer(Guid id)
        {
            var document = await _context
                .TransferCollection
                .AsQueryable()
                .Where(_ => _.TransferId == id)
                .FirstOrDefaultAsync();

            return document;
        }

        public async Task UpdateStatusError(Guid transferId, string erro)
        {
            var updateDefinition = Builders<ProcessingAccount>.Update
                .Set(_ => _.Erro, erro)
                .Set(_ => _.Status, "Error");

            await _context.TransferCollection.UpdateOneAsync(_ => _.TransferId == transferId, updateDefinition);

        }

        public async Task UpdateStatus(Guid transferId)
        {
            var updateDefinition = Builders<ProcessingAccount>.Update
                .Set(_ => _.Status, "Confirmed");

            await _context.TransferCollection.UpdateOneAsync(_ => _.TransferId == transferId, updateDefinition);
        }
    }
}
