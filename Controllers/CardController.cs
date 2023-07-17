
using ExpenseWizardApi.Services;
using Microsoft.AspNetCore.Mvc;


namespace ExpenseWizardApi.Controllers;


[ApiController]
[Route("api/cards")]
public class CardController:ControllerBase 
{
    private readonly ILogger<CardController> _logger;

    private readonly ICardService _cardService;

    public CardController(ILogger<CardController> logger, ICardService cardService)
    {
        _logger = logger;
        _cardService = cardService;
    }

    [HttpPost(Name = "CreateCard")]
    public async Task<IActionResult> CreateCard([FromBody] Stripe.Issuing.CardCreateOptions card)
    {
        var newCard = await _cardService.CreateCardAsync(card);
        
        return Ok(newCard);
    }


    [HttpGet("{userId}", Name = "GetCardsByUserId")]
    public async Task<IActionResult> GetCardsByUserId(string userId)
    {
        var cardHolders = await _cardService.GetCardsByUserId(userId);
        return Ok(cardHolders);
    }


}