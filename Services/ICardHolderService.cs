namespace ExpenseWizardApi.Services;

public interface ICardHolderService
{
    public Task<Stripe.Issuing.Cardholder> CreateCardHolderAsync(Stripe.Issuing.CardholderCreateOptions card);
    
}
