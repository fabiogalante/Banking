using AutoMapper;
using EventBus.Messages.Events;
using MassTransit;
using Microsoft.Extensions.Logging;
using Processing.Application.Features.FundTransfer.Command.Transfer;
using System;
using System.Threading.Tasks;
using MediatR;

namespace Processing.API.EventBusConsumer
{
    public class FundTransferConsumer : IConsumer<FundTransferEvent>
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly ILogger<FundTransferConsumer> _logger;

        public FundTransferConsumer(IMediator mediator, IMapper mapper, ILogger<FundTransferConsumer> logger)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task Consume(ConsumeContext<FundTransferEvent> context)
        {
            var command = _mapper.Map<FundTransferCommand>(context.Message);
            await _mediator.Send(command);
        }
    }
}
 