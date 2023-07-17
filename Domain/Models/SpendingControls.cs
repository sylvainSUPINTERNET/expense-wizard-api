using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

using Stripe;

namespace ExpenseWizardApi.Domain.Models;


public class SpendingControls
{
    [BsonElement("allowedCategories")]
    public required List<String> AllowedCategories { get; set; }

    [BsonElement("blockedCategories")]
    public required  List<String> BlockedCategories { get; set; }

    [BsonElement("spendingLimits")]
    public required  List<Stripe.Issuing.CardholderSpendingControlsSpendingLimit> SpendingLimits { get; set; }

    [BsonElement("spendingLimitsCurrency")]
    public required  String SpendingLimitsCurrency { get; set; }
}


