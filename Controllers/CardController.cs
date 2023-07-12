
using Microsoft.AspNetCore.Mvc;


namespace ExpenseWizardApi.Controllers;


[ApiController]
[Route("api/cards")]
public class CardController:ControllerBase 
{
    private readonly ILogger<CardController> _logger;

    public CardController(ILogger<CardController> logger)
    {
        _logger = logger;
    }


}