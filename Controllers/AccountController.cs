using ExpenseWizardApi.Services;
using Microsoft.AspNetCore.Mvc;


namespace ExpenseWizardApi.Controllers;


[ApiController]
[Route("api/accounts")]
public class AccountController:ControllerBase 
{
    private readonly ILogger<CardController> _logger;

    private readonly IAccount _accountService;

    public AccountController(ILogger<AccountController> logger, IAccount accountService)
    {
        _logger = logger;
        _accountService = accountService;
    }

    [HttpPost(Name = "CreateAccount")]
    public async Task<IActionResult> CreateAccount([FromBody] Stripe.AccountCreateOptions accountCreateOptions)
    {
        var newAccount = await _accountService.CreateAccountAsync(accountCreateOptions);
        
        return Ok(newAccount);
    }


    // [HttpGet("{userId}", Name = "LoginAccount")]
    // public async Task<IActionResult> GetCardsByUserId(string userId)
    // {
    //     var cardHolders = await _cardService.GetCardsByUserId(userId);
    //     return Ok(cardHolders);
    // }


}