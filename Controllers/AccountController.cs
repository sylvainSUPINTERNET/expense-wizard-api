using ExpenseWizardApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseWizardApi.Controllers;


[ApiController]
[Route("api/accounts")]
public class AccountController:ControllerBase 
{
    private readonly ILogger<AccountController> _logger;

    private readonly IAccountService _accountService;

    public AccountController(ILogger<AccountController> logger, IAccountService accountService)
    {
        _logger = logger;
        _accountService = accountService;
    }



    [HttpGet("balance", Name = "AccountBalance")]
    [Produces("application/json")]
    public async Task<IActionResult> GetBalance()
    {
        var balance = await _accountService.GetBalanceAsync();
        return Ok(balance);
    }


    // Since we use "custom" creation for account, we need to use a token ! 
    [HttpPost(Name = "CreateAccount")]
    [Produces("application/json")]
    public async Task<IActionResult> CreateAccount([FromForm] IFormCollection formData)
    {

        string tokenAccount = formData["token-account"];
        string tokenPerson = formData["token-person"];


        // _logger.LogInformation($"Account Create Options: {accountCreateOptions.ToString()}");

        var newAccount = await _accountService.CreateAccountAsync(tokenAccount, tokenPerson);
        
        return Ok(newAccount);
    }



}