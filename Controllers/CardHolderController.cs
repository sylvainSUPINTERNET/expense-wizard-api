
using ExpenseWizardApi.Models;
using ExpenseWizardApi.Services;
using Microsoft.AspNetCore.Mvc;


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
    public async Task<IActionResult> CreateCardHolder([FromBody] Stripe.Issuing.CardholderCreateOptions card)
    {
        var cardHolder = await _cardHolderService.CreateCardHolderAsync(card);
        // TODO => save id into database !
        return Ok(new CreateCardHolderResponseModel { cardHolderId = cardHolder.Id});
    }

}