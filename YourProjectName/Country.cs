using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http.Json;
using System.Linq;

public class Country
{
	public string Name { get; set; }
	public string CommonName { get; set; }
	public long Population { get; set; }
}
