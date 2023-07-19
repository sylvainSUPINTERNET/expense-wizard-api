
using ExpenseWizardApi.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Stripe;

namespace ExpenseWizardApi.Controllers;


[ApiController]
[Route("api/webhook")]
public class WebhookController:ControllerBase 
{
    private readonly ILogger<WebhookController> _logger;
    private readonly IOptions<ConfigSettings> _config;


    public WebhookController(ILogger<WebhookController> logger, IOptions<ConfigSettings> config)
    {
        _logger = logger;
        _config = config;

    }

    

    [HttpPost(Name = "StripeWebhook")]
    public async Task<IActionResult> webhookStripe()
    {
        var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
        string? endpointSecret = _config.Value.StripeWebhookSecret;


        Console.WriteLine($"endpointSecret: {endpointSecret}");

        if ( endpointSecret == null ) {
            _logger.LogInformation("Stripe Webhook Secret is null");
            return BadRequest();
        }

        try
        {
            Console.WriteLine($"json: {Request.Headers["Stripe-Signature"]}");

            var stripeEvent = EventUtility.ConstructEvent(json, 
                Request.Headers["Stripe-Signature"], 
                // https://dashboard.stripe.com/webhooks
                endpointSecret);

            // Handle the event
            if (stripeEvent.Type == Events.PaymentIntentSucceeded)
            {
                var paymentIntent = (PaymentIntent)stripeEvent.Data.Object;
                _logger.LogInformation($"PaymentIntent was successful for {paymentIntent.Amount}");
            }
            // ... handle other event types
            else
            {
                _logger.LogInformation($"Unhandled event type: {stripeEvent.Type}");
            }
        }
        catch (StripeException e)
        {
            _logger.LogInformation($"Error exception : {e.Message}");
            return BadRequest();
        }

        return Ok();
    }

}