
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ExpenseWizardApi.Domain.Models;


public class Card
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    
    // From request
    [BsonElement("userId")]
    [BsonRepresentation(BsonType.ObjectId)]
    public required string UserId { get; set; }

    [BsonElement("cardHolderId")]
    public required string CardHolderId { get; set; }

    [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    // From response

    [BsonElement("last4")]
    public required string Last4 { get; set; }

    [BsonElement("expMonth")]
    public required long ExpMonth { get; set; }

    [BsonElement("expYear")]
    public required long ExpYear { get; set; }

    [BsonElement("brand")]
    public required string Brand { get; set; }

    [BsonElement("currency")]
    public required string Currency { get; set; }

    // static
    [BsonElement("active")]
    public required string Active { get; set; }
    
    // TODO add support physic at some point
    [BsonElement("type")]
    public required string Type { get; set; }

}