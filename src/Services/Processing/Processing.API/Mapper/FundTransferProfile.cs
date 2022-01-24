using AutoMapper;
using EventBus.Messages.Events;
using Processing.Application.Features.FundTransfer.Command.Transfer;
using Processing.Domain.Entities;

namespace Processing.API.Mapper
{
    public class FundTransferProfile : Profile
    {
        public FundTransferProfile()
        {
            CreateMap<FundTransferCommand, FundTransferEvent>().ReverseMap();


            CreateMap<FundTransferCommand, ProcessingAccount>().ReverseMap();



            //FundTransferCommand->ProcessingAccount
            //Processing.Application.Features.FundTransfer.Command.Transfer.FundTransferCommand->Processing.Domain.Entities.ProcessingAccount

            //Type Map configuration:
            //FundTransferCommand->ProcessingAccount
            //Processing.Application.Features.FundTransfer.Command.Transfer.FundTransferCommand->Processing.Domain.Entities.ProcessingAccount

            //Destination Member:
            //TransferId
        }

    }
}
