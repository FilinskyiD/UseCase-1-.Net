using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace YourProjectName.Tests
{
    internal class CountryServiceTests
    {
        private readonly CountryService _countryService;

        public CountryServiceTests()
        {
            _countryService = new CountryService();
        }

        [Fact]
        public void TestFilterCountriesByName()
        {
            var countries = new List<Country>
            {
                new Country { Name = "Estonia", CommonName = "Estonia", Population = 1320000 },
                new Country { Name = "Spain", CommonName = "Spain", Population = 47000000 },
                new Country { Name = "Australia", CommonName = "Australia", Population = 25000000 }
            };

            var result = _countryService.FilterCountriesByName(countries, "st");
            Assert.Single(result);
            Assert.Equal("Estonia", result[0].Name);
        }

        [Fact]
        public void TestFilterCountriesByPopulation()
        {
            var countries = new List<Country>
            {
                new Country { Name = "Estonia", CommonName = "Estonia", Population = 1320000 },
                new Country { Name = "Spain", CommonName = "Spain", Population = 47000000 },
                new Country { Name = "Australia", CommonName = "Australia", Population = 25000000 }
            };

            var result = _countryService.FilterCountriesByPopulation(countries, 15);
            Assert.Single(result);
            Assert.Equal("Estonia", result[0].Name);
        }

        [Fact]
        public void TestSortCountriesByName()
        {
            var countries = new List<Country>
            {
                new Country { Name = "Spain", CommonName = "Spain", Population = 47000000 },
                new Country { Name = "Australia", CommonName = "Australia", Population = 25000000 },
                new Country { Name = "Estonia", CommonName = "Estonia", Population = 1320000 }
            };

            var result = _countryService.SortCountriesByName(countries, "ascend");
            Assert.Equal("Australia", result[0].Name);
            Assert.Equal("Estonia", result[1].Name);
            Assert.Equal("Spain", result[2].Name);
        }

        [Fact]
        public void TestLimitNumberOfRecords()
        {
            var countries = new List<Country>
            {
                new Country { Name = "Spain", CommonName = "Spain", Population = 47000000 },
                new Country { Name = "Australia", CommonName = "Australia", Population = 25000000 },
                new Country { Name = "Estonia", CommonName = "Estonia", Population = 1320000 }
            };

            var result = _countryService.LimitNumberOfRecords(countries, 2);
            Assert.Equal(2, result.Count);
            Assert.Equal("Spain", result[0].Name);
            Assert.Equal("Australia", result[1].Name);
        }
    }
}
