namespace ExpenseWizardApi.Services;

public interface ICardService 
{
    public Task<Stripe.Issuing.Card> CreateCardAsync (Stripe.Issuing.CardCreateOptions options);
}