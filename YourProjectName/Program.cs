using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1"));
}

app.UseHttpsRedirection();

app.MapGet("/api/countries", async (string? str1, int? num1, string? str2, int? num2) =>
{
    using var httpClient = new HttpClient();
    var response = await httpClient.GetAsync("https://restcountries.com/v3.1/all");

    if (!response.IsSuccessStatusCode)
    {
        return Results.BadRequest("Could not retrieve countries");
    }

    var countries = await response.Content.ReadFromJsonAsync<List<Country>>();

    // Here, you can use str1, num1, str2, num2 to filter or manipulate the countries list
    // ...

    return Results.Ok(countries);
}).Produces<List<Country>>(); // Adding type information for Swagger


app.Run();

public class Country
{
    public string Name { get; set; }
    public string Alpha2Code { get; set; }
    public string Alpha3Code { get; set; }
    public IEnumerable<string> Borders { get; set; }
}