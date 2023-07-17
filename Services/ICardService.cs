namespace ExpenseWizardApi.Services;

public interface ICardService 
{
    public Task<ExpenseWizardApi.Domain.Models.Card> CreateCardAsync (Stripe.Issuing.CardCreateOptions options);

    public Task<dynamic> GetCardsByUserId(string userId);

}