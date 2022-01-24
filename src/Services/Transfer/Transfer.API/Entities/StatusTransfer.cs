using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Transfer.API.Entities
{
    public class StatusTransfer
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public Guid TransferId { get; set; }
        public string Status { get; set; }
        public string Erro { get; set; }
        public DateTime CreationDate { get; set; }
        public string AccountOrigin { get; set; }
        public string AccountDestination { get; set; }
        public decimal Value { get; set; }
    }
}
