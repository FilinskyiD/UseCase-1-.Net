using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http.Json;
using System.Linq;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<CountryService>();

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

app.MapGet("/api/countries", async (CountryService countryService, string? searchString, int? populationInMillions, string? sortDirection, int? num2) =>
{
    using var httpClient = new HttpClient();
    var response = await httpClient.GetAsync("https://restcountries.com/v3.1/all");
    
    if (!response.IsSuccessStatusCode)
    {
        return Results.BadRequest("Could not retrieve countries");
    }

    var countries = await response.Content.ReadFromJsonAsync<List<Country>>();
    
    if (!string.IsNullOrEmpty(searchString))
    {
        countries = countryService.FilterCountriesByName(countries, searchString);
    }

    if (populationInMillions.HasValue)
    {
        countries = countryService.FilterCountriesByPopulation(countries, populationInMillions.Value);
    }

    if (!string.IsNullOrEmpty(sortDirection))
    {
        countries = countryService.SortCountriesByName(countries, sortDirection);
    }

    if (limit.HasValue)
    {
        countries = countryService.LimitNumberOfRecords(countries, limit.Value);
    }

    return Results.Ok(countries);
}).Produces<List<Country>>(); // Adding type information for Swagger

app.Run();
