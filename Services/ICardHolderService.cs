using ExpenseWizardApi.Domain.Models;

namespace ExpenseWizardApi.Services;

public interface ICardHolderService
{
    public Task<CardHolder> CreateCardHolderAsync(Stripe.Issuing.CardholderCreateOptions card);
    
    public Task<dynamic> GetCardHoldersByUserIdAsync(string userId);

}
