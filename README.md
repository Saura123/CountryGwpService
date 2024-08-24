# Country GWP Service

A .NET Core application that provides API endpoints for retrieving and calculating average GWP data by country and line of business.

## Prerequisites

- [.NET SDK 8.0](https://dotnet.microsoft.com/download).

## Getting Started

### Cloning the Repository

git clone https://github.com/Saura123/CountryGwpService.git
cd CountryGwpService

### Installing Dependencies 

Restore the project dependencies:

dotnet restore

## Running the Application.
dotnet run

The application will be available at http://localhost:9091 and swagger will open as default page

## To test the '/server/api/gwp/avg' endpoint using Swagger UI:

1) Open Swagger UI:
Navigate to http://localhost:9091 in your web browser.

2) Locate the Endpoint:
In Swagger UI, find and click on the POST /server/api/gwp/avg endpoint to expand its details.

3) Try Out the Endpoint:
 Click the "Try it out" button to enable input fields for the request.
 
4). Provide Input:
Enter the necessary data in the request body. This typically includes the country and line of business (LOB) values required by the endpoint.

5). Execute the Request:
Click the "Execute" button to send the request to the server.

6). View the Response:
The response from the server will appear in the "Response Body" section below. Here, you can review the output returned by the API.



## Running Tests

dotnet test
