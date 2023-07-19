namespace ExpenseWizardApi.Configuration;

public class ConfigSettings 
{

    public string? StripeKey {get; set;} = null;

    public string? StripeWebhookSecret {get; set;} = null;
    public string? ConnectionString {get; set;} = null;

    public string? DatabaseName {get; set;} = null;

    public string? CardHolderCollection {get; set;} = null;

    public string? CardCollection {get; set;} = null;
    
}