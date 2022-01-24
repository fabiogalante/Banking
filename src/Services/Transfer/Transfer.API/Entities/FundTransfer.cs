using System;

namespace Transfer.API.Entities
{
    public class FundTransfer
    {

        public string AccountOrigin { get; set; }
        public string AccountDestination { get; set; }
        public decimal Value { get; set; }
    }
}
