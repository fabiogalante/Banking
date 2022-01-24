using System.Text.Json.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Processing.Domain.Base
{
    public abstract class Entity
    {
        [BsonId]
        [JsonIgnore]
        public ObjectId Id { get; set; }

    }
}
