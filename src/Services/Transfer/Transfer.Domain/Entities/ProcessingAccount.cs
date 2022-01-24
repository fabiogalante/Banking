using System;
using Processing.Domain.Base;

namespace Processing.Domain.Entities
{
    public class ProcessingAccount : Entity
    {

        public ProcessingAccount()
        {
            CreationDate = DateTime.UtcNow;
        }

        public Guid TransferId { get; set; }
        public DateTime CreationDate { get; set; }
        public string AccountOrigin { get; set; }
        public string AccountDestination { get; set; }
        public decimal Value { get; set; }
        public string Status { get; set; }

        public string Erro { get; set; }

    }
}
