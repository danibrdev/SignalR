using Microsoft.OpenApi.Models;
using SignalRClient.Workers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

//builder.Services.ConfigureDependencyInjection(builder.Configuration); 
builder.Services.AddHostedService<SolicitacaoConsumer>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(s =>
{
    s.SwaggerDoc("v1", new OpenApiInfo { Title = "SignalRClient.Api", Version = "V1" }); 
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

app.Run();
