using ExpenseWizardApi.Configuration;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Stripe;

namespace ExpenseWizardApi.Services;


public class CardService : ICardService
{
    private readonly ILogger<CardService> _logger;

    private readonly  IOptions<ConfigSettings> _config;

    private readonly IMongoCollection<ExpenseWizardApi.Domain.Models.Card> _cardCollection;

    
    public CardService(ILogger<CardService> logger, IOptions<ConfigSettings> config)
    {
        _logger = logger;
        _config = config;

        var mongoClient = new MongoClient(
            config.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            config.Value.DatabaseName);

        _cardCollection = mongoDatabase.GetCollection<ExpenseWizardApi.Domain.Models.Card>(
            config.Value.CardCollection);
    }

    public async Task<Stripe.Issuing.Card> CreateCardAsync (Stripe.Issuing.CardCreateOptions options)
    {
            try
            {
                StripeConfiguration.ApiKey = _config.Value.StripeKey;

                var service = new Stripe.Issuing.CardService();

                // step 1
                Stripe.Issuing.Card resp = await service.CreateAsync(options);

        

                await _cardCollection.InsertOneAsync(
                new ExpenseWizardApi.Domain.Models.Card
                {
                    CardHolderId = resp.Cardholder.Id,
                });

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