using ExpenseWizardApi.Models.CardTokenCustom;

namespace ExpenseWizardApi.Services;

public interface IPaymentService 
{
    public Task<Stripe.Charge> CreatePayment(PaymentRequest stripeCardToken);

}