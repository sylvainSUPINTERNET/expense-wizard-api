
using ExpenseWizardApi.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;


namespace ExpenseWizardApi.Controllers;


[ApiController]
[Route("api/cardholders")]
public class CardHolderController:ControllerBase
{
    private readonly ILogger<CardController> _logger;

    private readonly ICardHolderService _cardHolderService;

    public CardHolderController(ILogger<CardController> logger, ICardHolderService cardHolderService)
    {
        _logger = logger;
        _cardHolderService = cardHolderService;
    }

    [HttpPost(Name = "CreateCardHolder")]
    public async Task<IActionResult> CreateCard([FromBody] Stripe.Issuing.CardholderCreateOptions card)
    {
        
        var cardHolder = await _cardHolderService.CreateCardHolderAsync(card);

        _logger.LogInformation(cardHolder.Id); // Example usage of the returned cardholder ID

        return Ok(card);
    }

}