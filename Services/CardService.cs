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

    public async Task<ExpenseWizardApi.Domain.Models.Card> CreateCardAsync (Stripe.Issuing.CardCreateOptions options)
    {
            try
            {
                StripeConfiguration.ApiKey = _config.Value.StripeKey;

                var service = new Stripe.Issuing.CardService();

                // step 1
                Stripe.Issuing.Card resp = await service.CreateAsync(options);


                _logger.LogInformation(resp.ToString());

                ExpenseWizardApi.Domain.Models.Card newCard = new ExpenseWizardApi.Domain.Models.Card {
                        CardHolderId = resp.Cardholder.Id,
                        // TODO change front end to send user id
                        UserId = "507f1f77bcf86cd799439011",
                        Last4 = resp.Last4,
                        ExpMonth = resp.ExpMonth,
                        ExpYear = resp.ExpYear,
                        Brand = resp.Brand,
                        Currency = resp.Currency,
                        Active = resp.Status,
                        Type = resp.Type
                };

                await _cardCollection.InsertOneAsync(newCard);

                return newCard;
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

    public async Task<dynamic> GetCardsByUserId(string userId)
    {
        var cards = await _cardCollection.Find($"{{ userId: ObjectId('{userId}') }}").ToListAsync();
        return cards;
    }

}