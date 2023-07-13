using ExpenseWizardApi.Configuration;
using ExpenseWizardApi.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<ConfigSettings>(
    builder.Configuration.GetSection("ExpenseWizardStore"));

// Add services to the container.
// https://learn.microsoft.com/en-us/aspnet/core/tutorials/first-mongo-app?view=aspnetcore-7.0&tabs=visual-studio
// Advise to use Singleton for the database connection
builder.Services.AddSingleton<ICardHolderService, CardHolderService>();
builder.Services.AddSingleton<ICardService, CardService>();

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
