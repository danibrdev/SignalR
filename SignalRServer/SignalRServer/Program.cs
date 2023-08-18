using Microsoft.OpenApi.Models;
using SignalRClient.Domain.Config;
using SignalRServer.Domain.Interfaces.Services;
using SignalRServer.Infra.CrossCutting;
using SignalRServer.Service;
using SignalRServer.Workers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddHttpClient<ISolicitacaoService, SolicitacaoService>(); 

builder.Services.Configure<RabbitMqConfig>(builder.Configuration.GetSection("RabbitMqConfig"));
builder.Services.AddHostedService<RabbitMqWorker>();
builder.Services.AddHostedService<Worker>();
builder.Services.AddSignalR();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(s =>
{
    s.SwaggerDoc("v1", new OpenApiInfo { Title = "SignalRServer.Api", Version = "V1" });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.UseHubs();

app.Run();