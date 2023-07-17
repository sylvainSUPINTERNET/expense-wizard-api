
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ExpenseWizardApi.Domain.Models;


public class CardHolder
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    [BsonElement("userId")]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? UserId { get; set; }

    [BsonElement("cardHolderId")]
    public string? CardHolderId { get; set; }
    
    [BsonElement("createdAt")]
    [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
    public DateTime CreatedAt { get; set; } = DateTime.Now;


    [BsonElement("SpendingControls")]
    public required SpendingControls SpendingControls { get; set; }

    [BsonElement("email")]
    public required String Email { get; set; }

    [BsonElement("name")]
    public required String Name { get; set; }
    
    [BsonElement("billing")]
    public required Billing Billing { get; set; }

    [BsonElement("individual")]
    public required Individual Individual { get; set; }

    [BsonElement("active")]
    public required String Active { get; set; } // active or inactive

    [BsonElement("type")]
    public required String Type { get; set; }
}


