using CallbackReceiverApi;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapPost("/callback", (UpdatedPraticaStatusEvent @event) =>
{
    Console.WriteLine($"Status of file with id {@event.PraticaId} has been updated to {@event.UpdatedStatus} on the date {@event.UpdatedDate}");
});

app.Run();
