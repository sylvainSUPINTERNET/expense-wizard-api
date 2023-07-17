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
        public async Task<CardHolder> CreateCardHolderAsync(Stripe.Issuing.CardholderCreateOptions options)
        {

            try
            {
                StripeConfiguration.ApiKey = _config.Value.StripeKey;

                var service = new Stripe.Issuing.CardholderService();

                // step 1
                var resp = await service.CreateAsync(options);


                _logger.LogInformation(resp.ToString());


                CardHolder cardHolder = new CardHolder {
                        CardHolderId = resp.Id,
                        // TODO change front end to send user id
                        UserId = "507f1f77bcf86cd799439011",
                        Email = resp.Email,
                        Name = resp.Name,
                        Billing = new Billing {
                            Address = new Domain.Models.Address
                            {
                                City = resp.Billing.Address.City,
                                Country = resp.Billing.Address.Country,
                                Line1 = resp.Billing.Address.Line1,
                                PostalCode = resp.Billing.Address.PostalCode,
                                State = resp.Billing.Address.State
                            },
                        },
                        Individual = new Individual {
                            Dob = new Domain.Models.Dob
                            {
                                Day = resp.Individual.Dob.Day,
                                Month = resp.Individual.Dob.Month,
                                Year = resp.Individual.Dob.Year
                            },
                            FirstName = resp.Individual.FirstName,
                            LastName = resp.Individual.LastName
                        },
                        SpendingControls = new SpendingControls {
                            AllowedCategories = resp.SpendingControls.AllowedCategories,
                            BlockedCategories = resp.SpendingControls.BlockedCategories,
                            SpendingLimits = resp.SpendingControls.SpendingLimits,
                            SpendingLimitsCurrency = resp.SpendingControls.SpendingLimitsCurrency
                        },
                        Active = resp.Status,
                        Type = resp.Type
                    };

                await _cardHoldersCollection.InsertOneAsync(cardHolder);

                return cardHolder;
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
    
        public async Task<dynamic> GetCardHoldersByUserIdAsync(string userId)
        {
            var cardHolders = await _cardHoldersCollection.Find($"{{ userId: ObjectId('{userId}') }}").ToListAsync();
            return cardHolders;
        }
    }

 
}
