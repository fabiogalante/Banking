using System;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using EventBus.Messages.Events;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Transfer.API.Db;
using Transfer.API.Entities;
using Transfer.API.Response;

namespace Transfer.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class TransferController : ControllerBase
    {
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly IMapper _mapper;

        public TransferController(IPublishEndpoint publishEndpoint, IMapper mapper)
        {
            _publishEndpoint = publishEndpoint;
            _mapper = mapper;
        }


        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Accepted)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> TransferMoney([FromBody] FundTransfer fundTransfer)
        {
            // send event to rabbitmq
            var eventMessage = _mapper.Map<FundTransferEvent>(fundTransfer);
            await _publishEndpoint.Publish<FundTransferEvent>(eventMessage);
            return Accepted(new TransferResponse
            {
                TransactionId = eventMessage.TransferId
            });
        }


        [HttpGet("{guid:guid}")]
        public async Task<ActionResult> GetStatus(Guid guid)
        {
            var accountDb = new AccountDb();
            var transferAccount = await accountDb.ObterStatus(guid);
            return Ok(new {transferAccount.Status, transferAccount.Erro});
        }
    }
}
