using System;
using System.Threading.Tasks;
using Processing.Domain.Entities;

namespace Processing.Domain.Repositories.CreateTransfer
{
    public interface IProcessingRepository
    {
        Task CreateTransfer(ProcessingAccount processingAccount);

        Task<ProcessingAccount> GetTransfer(Guid id);
        Task UpdateStatusError(Guid transferId,string erro);

        Task UpdateStatus(Guid transferId);
    }
}
