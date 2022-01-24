using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Account.Service.Client;
using Account.Service.Request;
using Account.Service.Response;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Processing.Domain.Entities;
using Processing.Domain.Repositories.CreateTransfer;

namespace Processing.Application.Features.FundTransfer.Command.Transfer
{
    public class FundTransferHandler : IRequestHandler<FundTransferCommand, Unit>
    {
        private readonly IMapper _mapper;
        private readonly IAccount _account;
        private readonly IProcessingRepository _processingRepository;
        private readonly ILogger<FundTransferHandler> _logger;

        public FundTransferHandler(IMapper mapper, IAccount account, IProcessingRepository processingRepository, ILogger<FundTransferHandler> logger)
        {
            _mapper = mapper;
            _account = account;
            _processingRepository = processingRepository;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }


        public async Task<Unit> Handle(FundTransferCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Starting transfer {request.TransferId}");

            var transfer = _mapper.Map<ProcessingAccount>(request);
            transfer.Status = "In Queue";

            await _processingRepository.CreateTransfer(transfer);

            if (await ValidateOrigin(transfer) && await ValidateDestination(transfer)) await TransferMoney(transfer);

            return Unit.Value;
        }

        private async Task TransferMoney(ProcessingAccount transfer)
        {
            if (await TransferOrigin(transfer) && await TransferDestination(transfer))
                await _processingRepository.UpdateStatus(transfer.TransferId);
        }

        private async Task<bool> TransferDestination(ProcessingAccount transfer)
        {
            var responseOrigin = await _account.Account(new TransferAccount
            {
                AccountNumber = transfer.AccountDestination,
                Type = CreditDebit.Credit,
                Value = transfer.Value
            });

           

            if (responseOrigin.IsSuccessStatusCode) return true;

            _logger.LogError(
                $"Id - {transfer.TransferId} - Error Transfer Destination - Account - {transfer.AccountDestination} - {responseOrigin.ReasonPhrase}");

            await _processingRepository.UpdateStatusError(transfer.TransferId,
                $"Error Transfer Destination - Account - {transfer.AccountDestination} - {responseOrigin.ReasonPhrase}");

            return false;

        }

        private async Task<bool> TransferOrigin(ProcessingAccount transfer)
        {
            var responseOrigin = await _account.Account(new TransferAccount
            {
                AccountNumber = transfer.AccountOrigin,
                Type = CreditDebit.Debit,
                Value = transfer.Value
            });

            if (responseOrigin.IsSuccessStatusCode) return true;

            if (responseOrigin.StatusCode == HttpStatusCode.BadRequest)
            {
                await _processingRepository.UpdateStatusError(transfer.TransferId,
                    $"Not enough balance - {transfer.AccountOrigin} - {responseOrigin.ReasonPhrase}");
            }

            _logger.LogError($"Id - {transfer.TransferId} - Error Transfer Origem - Account - {transfer.AccountOrigin} - {responseOrigin.ReasonPhrase}");

            await _processingRepository.UpdateStatusError(transfer.TransferId,
                $"Error Transfer Origem - Account - {transfer.AccountOrigin} - {responseOrigin.ReasonPhrase}");

            return false;

        }


        private async Task<bool> ValidateOrigin(ProcessingAccount account)
        {

            var originResponse = await _account.GetAccount(account.AccountOrigin);

            _logger.LogInformation($"ValidateOrigin : {account.AccountOrigin}");

            if (!originResponse.IsSuccessStatusCode)
            {
                await _processingRepository.UpdateStatusError(account.TransferId,
                    $"Origin Invalid Account - {account.AccountOrigin} - {originResponse.ReasonPhrase}");

                _logger.LogError(
                    $"Origin Invalid Account - {account.AccountOrigin} - {originResponse.ReasonPhrase}");

                return false;
            }

            var content = await originResponse.Content.ReadAsStringAsync();
            var deserializedProcessing = JsonConvert.DeserializeObject<AccountResponse>(content);
            if (account.Value <= deserializedProcessing.Balance) return true;
            await _processingRepository.UpdateStatusError(account.TransferId,
                $"Not enough balance - {deserializedProcessing.Balance}");
            return false;

        }

        private async Task<bool> ValidateDestination(ProcessingAccount account)
        {
            var originResponse = await _account.GetAccount(account.AccountDestination);

            _logger.LogInformation($"ValidateDestination : {account.AccountOrigin}");

            if (originResponse.IsSuccessStatusCode) return true;
            await _processingRepository.UpdateStatusError(account.TransferId, $"Distination Invalid Account - {account} - {originResponse.ReasonPhrase}");

            _logger.LogError(
                $"Distination Invalid Account - {account.AccountOrigin} - {originResponse.ReasonPhrase}");

            return false;
        }

        
    }
}
