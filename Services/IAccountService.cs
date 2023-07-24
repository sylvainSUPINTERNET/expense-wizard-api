using ExpenseWizardApi.Domain.Models;

namespace ExpenseWizardApi.Services;

public interface IAccountService
{
    public Task<Stripe.Account> CreateAccountAsync(string accountToken, string personToken);
    

    public Task<Stripe.Balance> GetBalanceAsync();

}
