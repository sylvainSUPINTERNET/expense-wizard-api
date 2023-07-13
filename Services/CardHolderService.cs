using ExpenseWizardApi.Configuration;
using ExpenseWizardApi.Domain.Models;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using Stripe;

namespace ExpenseWizardApi.Services
{
    public class CardHolderService : ICardHolderService
    {

        private readonly  IOptions<ConfigSettings> _config;
        private readonly ILogger<CardHolderService> _logger;

        private readonly IMongoCollection<CardHolder> _cardHoldersCollection;

        public CardHolderService(ILogger<CardHolderService> logger, IOptions<ConfigSettings> config)
        {
            _logger = logger;
            _config = config;

            var mongoClient = new MongoClient(
                config.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                config.Value.DatabaseName);


            _cardHoldersCollection = mongoDatabase.GetCollection<CardHolder>(
                config.Value.CardHolderCollection);
            
        }
        /**
         * Support only for individual card holder and company card holders
         * 
         * @param card The card holder to create
         * @return The card holder created
         */
        public async Task<Stripe.Issuing.Cardholder> CreateCardHolderAsync(Stripe.Issuing.CardholderCreateOptions options)
        {

            try
            {
                StripeConfiguration.ApiKey = _config.Value.StripeKey;

                var service = new Stripe.Issuing.CardholderService();

                // step 1
                var resp = await service.CreateAsync(options);


                // step 2
                await _cardHoldersCollection.InsertOneAsync(new CardHolder
                {
                    CardHolderId = resp.Id,

                    // TODO : replace this mock with real user id
                    UserId = ObjectId.GenerateNewId().ToString()
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
}
