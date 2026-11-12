var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddValidation();

var app = builder.Build();

app.MapOpenApi();

app.MapPost("/check-ins", (CheckInRequest checkIn, ILogger<Program> logger) =>
{
    logger.LogInformation($"Check-in registrado: CPF {checkIn.Cpf}, " +
        $"data e horário {checkIn.DataHora:O}, local {checkIn.Local}");
    return Results.NoContent();
})
.WithName("RegistrarCheckIn");

app.Run();