
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ExpenseWizardApi.Domain.Models;


public class Dob
{
    [BsonElement("day")]
    public long? Day { get; set; }

    [BsonElement("month")]
    public long? Month { get; set; }


    [BsonElement("year")]
    public long? Year { get; set; }

}


