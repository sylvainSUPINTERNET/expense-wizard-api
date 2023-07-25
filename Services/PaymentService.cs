using ExpenseWizardApi.Configuration;
using ExpenseWizardApi.Models.CardTokenCustom;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Stripe;

namespace ExpenseWizardApi.Services;


public class PaymentService : IPaymentService
{
    private readonly ILogger<PaymentService> _logger;

    private readonly  IOptions<ConfigSettings> _config;


    public PaymentService ( ILogger<PaymentService> logger, IOptions<ConfigSettings> config)
    {
        _logger = logger;
        _config = config;
    }

    public async Task<Stripe.Charge> CreatePayment(PaymentRequest stripeCardToken)
    {
        
        StripeConfiguration.ApiKey = _config.Value.StripeKey;

        var options = new ChargeCreateOptions
        {
            Amount = (long?)(stripeCardToken.Amount * 100),
            Currency = stripeCardToken.Currency,
            Source = stripeCardToken.CardTokenCustom.Id
        }; 
        var service = new ChargeService();

        Charge charge;

        try 
        {
            charge = await service.CreateAsync(options);
        } 
        catch (StripeException e) 
        {
            _logger.LogError(e.Message);
            return null;
        }

        return charge;
    }

}