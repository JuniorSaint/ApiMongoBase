using System;
using MongoDB.Bson.Serialization.Attributes;

namespace Api.Domain.Entities
{
    public class BaseEntities
    {
        [BsonId]
        public Guid Id { get; set; }
        public DateTime? UpdatedAt { get; set; }

        private DateTime? _createdAt { get; set; }
        public DateTime? CreatedAt
        {
            get { return _createdAt; }
            set { _createdAt = (value == null ? DateTime.UtcNow : value); }
        }
    }
}
