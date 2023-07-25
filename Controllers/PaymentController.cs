using ExpenseWizardApi.Models.CardTokenCustom;
using ExpenseWizardApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseWizardApi.Controllers;


[ApiController]
[Route("api/payment")]
public class PaymentController:ControllerBase 
{

    private readonly ILogger<PaymentController> _logger;

    private readonly IPaymentService _paymentService;

    public PaymentController (ILogger<PaymentController> logger, IPaymentService paymentService) 
    {
        _logger = logger;
        _paymentService = paymentService;

    }

    [HttpPost(Name = "CreatePayment")]
    [Produces("application/json")]
    public async Task<IActionResult> CreatePayment([FromBody] PaymentRequest stripeCardToken)
    {    
        
        Stripe.Charge charge = await _paymentService.CreatePayment(stripeCardToken);

        return Ok(charge);
    }
}