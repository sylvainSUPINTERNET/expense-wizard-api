
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ExpenseWizardApi.Domain.Models;


public class Address
{

    [BsonElement("city")]
    public required String City { get; set; }

    [BsonElement("country")]
    public required String Country { get; set; }

    [BsonElement("line1")]
    public required String Line1 { get; set; }

    [BsonElement("postalCode")]
    public required String PostalCode { get; set; }

    
    [BsonElement("state")]
    public required String State { get; set; }
}
