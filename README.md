# Product Management Application
C# .NET Core API which can be used to create, view, update and delete Products and Product Options.

## Development Approach
1. Determine API design (routes, contracts and internal structure) and identified hosting models.
2. Implemented and tested happy path API workflows (Product and Product Options CRUD actions)
3. Created a docker-compose file to allow the application to be ran easier locally.
4. Implemented validations, improved error handling and included Swagger support to make testing easier.
5. Conducted end-to-end testing.
6. Hosted the application on an Azure App Service to showcase.
7. Wrote documentation

## Code Design
- The API is separated into the following layers
	- API (request entry point and app startup logic)
	- Business (validation and other business logic on a per domain entity basis)
	- Data (product repository to access product and product options data sources)
	- Contract (domain model definitions and attribute validations)
- A unit test project exists for the Business layer to validate end-to-end workflows
- Strong use of dependency injection and ensuring separated class responsibilities
	- Simplifies unit testing
	- Improves extendability and maintainability.
	
## Quality
- Business layer unit tests covering all validations and success cases.
- End to end testing was performed for all success and validation cases.

## Error Handling
- Attribute validation on the request models used in the incoming HTTP request. This is handled in a custom response to sanitise the error information.
- Custom defined error messages for all validation failures
- Global Exception Handler which will provide a sanitised error message when an exception is thrown.

## Security
- Users of the API cannot update the identifiers for any of the domain entities.
- Stack traces are hidden from end user.
- Security flaw in exposing Swagger UI and API on a public app service to assist in showcasing.

## Infra/Operability
- Solution can be ran as a docker compose to simplify local development
- Application is containerised to simplify hosting and deployment
- Publicly accessible on an Azure App Service instance.
- Swagger UI support to easily test the API as an end-user.

## How to run locally
To perform the following you need a copy of the source code and docker configured.
- In a cmd tool with docker support navigate to root dir (README location): `cd ./ProductManagementApplication`
- Start the application by running: `docker-compose up`
- Access API endpoints via Swagger UI:
    - Swagger UI (for API): http://localhost:8081/swagger/index.html

## Azure Hosted URL
- Swagger UI: https://product-management-api.azurewebsites.net/swagger/index.html

## Effort Estimates
- 0.75 hours planning
- 5 hours to complete the base API implementation.
- 1 hour to create docker compose and implement error handling.
- 0.5 hours to deploy to Azure
- 0.75 hours to update documentation.

## Suggestions
- Move the solution to .NET 6 for Long-Term Support when it is released.
- Tie a business identifier, e.g. Name, to products and product options to determine uniqueness as an end-user.
- Remove ProductId from ProductOption domain model and use path parameters to identify relationship, was included to simplify handling in data layer due.
- Put the Azure App Service behind a private endpoint or API gateway to proxy requests to API instead of leaving the app service public. Was left public to showcase.
- Move contract to versioned NuGet package.
- API Authentication and Authorisation.
- Improve Resiliency using exponential retries to retry failed requests. 
- Move to a managed database service or another form of persistent storage and utilize a cache.
- Use more explicit HTTP response codes depending on the outcome of a request