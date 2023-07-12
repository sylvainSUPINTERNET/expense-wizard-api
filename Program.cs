using ExpenseWizardApi.Services;
using Stripe;
// StripeConfiguration.ApiKey = "sk_test_51N6dD2G9WAND04yWkEIRhFTbhQuFs6r6hHGqDmXoMVpdeN16xWlPMnub2bNp22uwzQ22fEaks10bG8tIk6jaGxeU00udaG1iux";

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddScoped<ICardHolderService, CardHolderService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();



// https://stripe.com/docs/api?lang=dotnet


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
