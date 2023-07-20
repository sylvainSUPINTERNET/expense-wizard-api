using ExpenseWizardApi.Domain.Models;

namespace ExpenseWizardApi.Services;

public interface IAccountService
{
    public Task<Stripe.AccountCreateOptions> CreateAccountAsync(Stripe.AccountCreateOptions accountCreateOptions);
    

}
