using ExpenseWizardApi.Models.CardHolder;
using Stripe;

namespace ExpenseWizardApi.Services
{
    public class CardHolderService : ICardHolderService
    {

        private readonly ILogger<CardHolderService> _logger;

        public CardHolderService(ILogger<CardHolderService> logger)
        {
            _logger = logger;
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
                Console.WriteLine(options);
                var service = new Stripe.Issuing.CardholderService();
                return await service.CreateAsync(options);
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
