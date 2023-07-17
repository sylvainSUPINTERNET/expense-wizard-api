
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ExpenseWizardApi.Domain.Models;


public class Billing
{

    [BsonElement("address")]
    public required Address Address { get; set; }
}
