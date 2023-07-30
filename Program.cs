using ExpenseWizardApi.Configuration;
using ExpenseWizardApi.Services;


var builder = WebApplication.CreateBuilder(args);

// TODO => update allowed origins
var policyCors = "AllowedOrigins";
builder.Services.AddCors( options => {
    options.AddPolicy(policyCors, builder => {
        builder.WithOrigins("*")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

builder.Services.Configure<ConfigSettings>(
    builder.Configuration.GetSection("ExpenseWizardStore"));

// Add services to the container.
// https://learn.microsoft.com/en-us/aspnet/core/tutorials/first-mongo-app?view=aspnetcore-7.0&tabs=visual-studio
// Advise to use Singleton for the database connection
builder.Services.AddSingleton<ICardHolderService, CardHolderService>();
builder.Services.AddSingleton<ICardService, CardService>();
builder.Services.AddSingleton<IAccountService, AccountService>();
builder.Services.AddScoped<IPaymentService, PaymentService>();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();



// https://stripe.com/docs/api?lang=dotnet

app.UseCors(policyCors);

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
