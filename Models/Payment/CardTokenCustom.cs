namespace ExpenseWizardApi.Models.CardTokenCustom;


public class CardTokenCustom
{
    public required string Id {get;set;}

    public required string Client_ip {get;set;}

    public required CardCustom Card {get;set;}

}