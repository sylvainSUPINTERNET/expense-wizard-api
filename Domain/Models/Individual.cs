
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ExpenseWizardApi.Domain.Models;


public class Individual
{
    [BsonElement("dob")]
    public required Dob Dob { get; set; }

    [BsonElement("firstName")]
    public required String FirstName { get; set; }

    [BsonElement("lastName")]
    public required String LastName { get; set; }

}


