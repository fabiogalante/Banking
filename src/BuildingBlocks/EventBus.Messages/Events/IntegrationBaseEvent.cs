using System;

namespace EventBus.Messages.Events
{
    public class IntegrationBaseEvent
    {
        public IntegrationBaseEvent()
        {
            TransferId = Guid.NewGuid();
            CreationDate = DateTime.UtcNow;
        }

        public IntegrationBaseEvent(Guid transferId, DateTime createDate)
        {
            TransferId = transferId;
            CreationDate = createDate;
        }

        public Guid TransferId { get; private set; }

        public DateTime CreationDate { get; private set; }
    }
}
