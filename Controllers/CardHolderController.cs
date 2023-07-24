
using ExpenseWizardApi.Services;
using Microsoft.AspNetCore.Mvc;


namespace ExpenseWizardApi.Controllers;


[ApiController]
[Route("api/cardholders")]
public class CardHolderController:ControllerBase
{
    private readonly ILogger<CardHolderController> _logger;

    private readonly ICardHolderService _cardHolderService;

    public CardHolderController(ILogger<CardHolderController> logger, ICardHolderService cardHolderService)
    {
        _logger = logger;
        _cardHolderService = cardHolderService;
    }

    [HttpPost(Name = "CreateCardHolder")]
    public async Task<IActionResult> CreateCardHolder([FromBody] Stripe.Issuing.CardholderCreateOptions card)
    {
        var cardHolder = await _cardHolderService.CreateCardHolderAsync(card);
        // TODO => save id into database !
        return Ok(cardHolder);
    }


    [HttpGet("{userId}", Name = "GetCardHoldersByUserId")]
    public async Task<IActionResult> GetCardHoldersByUserId(string userId)
    {
        var cardHolders = await _cardHolderService.GetCardHoldersByUserIdAsync(userId);
        return Ok(cardHolders);
    }
}