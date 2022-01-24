using System;
using MediatR;

namespace Processing.Application.Features.FundTransfer.Command.Transfer
{
    public class FundTransferCommand : IRequest<Unit>
    {
        public Guid TransferId { get; set; }
        public string AccountOrigin { get; set; }
        public string AccountDestination { get; set; }
        public decimal Value { get; set; }

    }
}
