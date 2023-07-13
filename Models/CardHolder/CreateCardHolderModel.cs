namespace ExpenseWizardApi.Models.CardHolder;

public class CardModel 
{
    public required string Type {get;set;}

    public required string Name {get;set;}

    public required string Email {get;set;}

    public required string PhoneNumber {get;set;}

    public required Stripe.Issuing.CardholderBillingOptions Billing {get;set;}

    public required Stripe.AddressOptions Address {get;set;}
    
}