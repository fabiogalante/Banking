using AutoMapper;
using EventBus.Messages.Events;
using Transfer.API.Entities;

namespace Transfer.API.Mapper
{
    public class FundTransferProfile : Profile
    {
        public FundTransferProfile()
        {
            CreateMap<FundTransfer, FundTransferEvent>().ReverseMap();

        }

    }
}
