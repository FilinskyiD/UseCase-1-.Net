namespace YourProjectName
{
    public class CountryService
    {
        public List<Country> FilterCountriesByName(List<Country> countries, string searchString) => countries
            .Where(country => country.Name is not null && country.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase)
            || country.CommonName is not null && country.CommonName.Contains(searchString, StringComparison.OrdinalIgnoreCase))
            .ToList();


        public List<Country> FilterCountriesByPopulation(List<Country> countries, int populationInMillions) => countries
            .Where(country => country.Population < populationInMillions * 1_000_000).ToList();

        public List<Country> SortCountriesByName(List<Country> countries, string sortDirection) => sortDirection.ToLower() switch
        {
            "ascend" => countries.OrderBy(country => country.Name).ToList(),
            "descend" => countries.OrderByDescending(country => country.Name).ToList(),
            _ => countries
        };

        public List<Country> LimitNumberOfRecords(List<Country> countries, int limit) => countries.Take(limit).ToList();

    }
}