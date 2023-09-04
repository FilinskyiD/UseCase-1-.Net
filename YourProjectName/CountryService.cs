using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http.Json;
using System.Linq;

public class CountryService
{
    public static List<Country> FilterCountriesByName(List<Country> countries, string searchString) => countries
        .Where(country => (country.Name is not null && country.Name.IndexOf(searchString, System.StringComparison.OrdinalIgnoreCase) >= 0)
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

}
