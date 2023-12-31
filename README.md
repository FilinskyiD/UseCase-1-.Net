Country API with Filtering, Sorting, and Pagination
Application Description
This application serves as a RESTful API, built using .NET 6 and C# 10, which fetches country data from the "REST Countries" public API. It provides enhanced functionalities like filtering by country name or population, sorting by country name in ascending or descending order, and pagination to limit the number of records. The purpose of this application is to demonstrate how to effectively implement these common API features using Minimal API in .NET Core.

The application is designed following the best practices of API development. It leverages dependency injection for service classes, making the codebase more maintainable and testable. The functions for filtering, sorting, and pagination have been encapsulated into a service class, and the application also includes a suite of unit tests for verifying the functionality of these methods.

Running the Application Locally
Clone the repository to your local machine.
bash
Copy code
git clone <repository_url>
Navigate into the directory where the solution file (.sln) is located.
bash
Copy code
cd path/to/directory
Restore the NuGet packages.
Copy code
dotnet restore
Build the application.
Copy code
dotnet build
Run the application.
arduino
Copy code
dotnet run
API Endpoint Examples
Below are some examples demonstrating how to use the developed API endpoint.

Fetch all countries:

bash
Copy code
GET /api/countries
Filter countries containing the string 'st':

bash
Copy code
GET /api/countries?searchString=st
Filter countries with a population less than 15 million:

bash
Copy code
GET /api/countries?populationInMillions=15
Sort countries by name in ascending order:

bash
Copy code
GET /api/countries?sortDirection=ascend
Sort countries by name in descending order:

bash
Copy code
GET /api/countries?sortDirection=descend
Limit the number of returned records to 10:

bash
Copy code
GET /api/countries?limit=10
Combine filtering by name and population:

bash
Copy code
GET /api/countries?searchString=st&populationInMillions=20
Combine sorting and pagination:

bash
Copy code
GET /api/countries?sortDirection=ascend&limit=5
Combine all functionalities (filtering by name, filtering by population, sorting, and pagination):

bash
Copy code
GET /api/countries?searchString=st&populationInMillions=20&sortDirection=ascend&limit=5
Fetch all countries, but only if they have a population less than 50 million and sort them by name in ascending order:

bash
Copy code
GET /api/countries?populationInMillions=50&sortDirection=ascend