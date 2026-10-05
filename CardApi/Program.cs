using CardApi.Data;
using CardApi.Rules;
using CardApi.Services;
using CardApi.Strategy;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<ICardService, CardService>();

var strategyType = typeof(ICardActionStrategy);

var strategyTypes = Assembly
    .GetExecutingAssembly()
    .GetTypes()
    .Where(type =>
        type is { IsClass: true, IsAbstract: false }
        && strategyType.IsAssignableFrom(type));

foreach (var type in strategyTypes)
{
    builder.Services.AddScoped(
        typeof(ICardActionStrategy),
        type);
}


builder.Services.AddScoped<ICardActionRuleEngine, CardActionRuleEngine>();

builder.Services.AddScoped<ICardActionService, CardActionService>();
builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

