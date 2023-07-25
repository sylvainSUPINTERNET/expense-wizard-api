using System.Text.Json.Serialization;

namespace ExpenseWizardApi.Models.CardTokenCustom;

public class PaymentRequest
{
    [JsonPropertyName("stripeCardToken")]
    public required CardTokenCustom CardTokenCustom {get;set;}

    public required float Amount {get;set;}

    public required string Currency {get;set;}
}
