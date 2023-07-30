
using ExpenseWizardApi.Configuration;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Stripe;

namespace ExpenseWizardApi.Services;

public class AccountService : IAccountService
{
    private readonly ILogger<AccountService> _logger;

    private readonly  IOptions<ConfigSettings> _config;


    public AccountService ( ILogger<AccountService> logger, IOptions<ConfigSettings> config)
    {
        _logger = logger;
        _config = config;
    }



    public async Task<Balance> GetBalanceAsync()
    {

        StripeConfiguration.ApiKey = _config.Value.StripeKey;

        var service = new BalanceService();
        var resp = await service.GetAsync();

        return resp;
    }

    
    

    // TODO : to remove ?
    // https://stripe.com/docs/connect/express-accounts
    public async Task<Stripe.Account> CreateAccountAsync(string accountToken, string personToken)
    {   
        
        try
            {
                StripeConfiguration.ApiKey = _config.Value.StripeKey;

                // var options = new AccountCreateOptions
                // {
                //     Type = "custom",
                //     Country = "FR",
                //     Email = "tes@test.com",
                //     BusinessType = "individual",

                // };

                // options.AccountToken = accountToken;

                // options.Capabilities = new AccountCapabilitiesOptions
                // {
                //     CardPayments = new AccountCapabilitiesCardPaymentsOptions
                //     {
                //         Requested = true
                //     },
                //     Transfers = new AccountCapabilitiesTransfersOptions
                //     {
                //         Requested = truex
                //     },
                //     CardIssuing = new AccountCapabilitiesCardIssuingOptions
                //     {
                //         Requested = true
                //     },
                //     SepaDebitPayments = new AccountCapabilitiesSepaDebitPaymentsOptions
                //     {
                //         Requested = true
                //     },
                    
                // };

                // var service = new Stripe.AccountService();

                // // step 1
                // var resp = await service.CreateAsync(options);


                // _logger.LogInformation(resp.ToString());


                // var options = new AccountCreateOptions
                // {
                //     Country = "FR",
                //     Type = "custom",
                //     Capabilities = new AccountCapabilitiesOptions
                //     {
                //         CardPayments = new AccountCapabilitiesCardPaymentsOptions { Requested = true },
                //         Transfers = new AccountCapabilitiesTransfersOptions { Requested = true },
                //         SepaDebitPayments = new AccountCapabilitiesSepaDebitPaymentsOptions { Requested = true },
                //         CardIssuing = new AccountCapabilitiesCardIssuingOptions { Requested = true }
                //     },
                //     AccountToken = accountToken,
                // };


                    var xd = new TokenService();
                    Token stripeToken = xd.Get(accountToken); // replace with your token ID
                    dynamic data = xd.Get(stripeToken.Id); // replace with your token ID

                    Console.WriteLine(stripeToken.ToString()); // Will print the type of token, e.g., "account"
                                        Console.WriteLine(data.ToString()); // Will print the type of token, e.g., "account"


                    var options = new AccountCreateOptions
                    {
                        Country = "FR",
                        Type = "custom",
                        AccountToken = accountToken,
                        Capabilities = new AccountCapabilitiesOptions
                        {
                            CardPayments = new AccountCapabilitiesCardPaymentsOptions { Requested = true },
                            Transfers = new AccountCapabilitiesTransfersOptions { Requested = true },
                            SepaDebitPayments = new AccountCapabilitiesSepaDebitPaymentsOptions { Requested = true },
                            CardIssuing = new AccountCapabilitiesCardIssuingOptions { Requested = true }
                        },
                    };

                var service = new Stripe.AccountService();
                var resp = await service.CreateAsync(options);
                return resp;    

            }
            catch (StripeException e)
            {
                // Handle specific Stripe API exceptions
                _logger.LogInformation($"Stripe Error: {e.Message}");
                // Handle or log the error as needed
                throw; // Rethrow the exception to be handled further up the call stack
            }
            catch (Exception e)
            {
                // Handle other exceptions
                _logger.LogInformation($"Error: {e.Message}");

                // Handle or log the error as needed
                throw; // Rethrow the exception to be handled further up the call stack
            }
        }

}
