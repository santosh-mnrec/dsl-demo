using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Orsted.WindTurbine.DSL;

var builder = WebApplication.CreateBuilder(args); ;


// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("/convert", async context =>
{
    using var reader = new StreamReader(context.Request.Body);
    string turbineInput = await reader.ReadToEndAsync();
    Console.WriteLine(turbineInput);
    
    var parserHelper = new TurbineParserHelper();
    var turbine = parserHelper.ParseTurbine(turbineInput);

    TurbineVisitor visitor = new();
    var parsedTurbine = visitor.Visit(turbine);

    var data = parserHelper.ConvertToJSON(turbineInput); // Assuming ConvertToJSON takes a parsed turbine object
    
    Console.WriteLine(data);

    context.Response.ContentType = "application/json";
    await context.Response.WriteAsync(data.ToString());
})


.WithOpenApi();

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
