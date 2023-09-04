using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http.Json;
using System.Linq;

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

app.MapGet("/api/countries", async (string? searchString, int? populationInMillions, string? sortDirection, int? num2) =>
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
        countries = FilterCountriesByName(countries, searchString);
    }

    if (populationInMillions.HasValue)
    {
        countries = FilterCountriesByPopulation(countries, populationInMillions.Value);
    }

    if (!string.IsNullOrEmpty(sortDirection))
    {
        countries = SortCountriesByName(countries, sortDirection);
    }

    if (limit.HasValue)
    {
        countries = LimitNumberOfRecords(countries, limit.Value);
    }

    return Results.Ok(countries);
}).Produces<List<Country>>(); // Adding type information for Swagger

public class Country
{
    public string Name { get; set; }
    public string CommonName { get; set; }
    public long Population { get; set; }
}

public static List<Country> FilterCountriesByName(List<Country> countries, string searchString) => countries
    .Where(country => (country.Name is not null && country.Name.IndexOf(searchString, System.StringComparison.OrdinalIgnoreCase) >= 0 )
    || (country.CommonName is not null && country.CommonName.IndexOf(searchString, System.StringComparison.OrdinalIgnoreCase) >= 0))
    .ToList();


public static List<Country> FilterCountriesByPopulation(List<Country> countries, int populationInMillions) => countries
    .Where(country => country.Population < populationInMillions * 1_000_000).ToList();

public static List<Country> SortCountriesByName(List<Country> countries, string sortDirection) => sortDirection.ToLower() switch
{
    "ascend" => countries.OrderBy(country => country.Name).ToList(),
    "descend" => countries.OrderByDescending(country => country.Name).ToList(),
    _ => countries
};

public static List<Country> LimitNumberOfRecords(List<Country> countries, int limit) => countries.Take(limit).ToList();

app.Run();
