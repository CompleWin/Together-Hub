using System.Text;

Console.OutputEncoding = Encoding.Unicode;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddApiServices(builder.Configuration)
    .AddInfastructureServices(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    await app.InitializeDatabaseAsync();
}

app.UseApiServices();

app.Run();

