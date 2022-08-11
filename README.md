## Refactor Summary

### Overview 
This project is for testing skill purposes. Was forked from https://github.com/Paramo-Tech/Sat.Recruitment and refactored.

### Architecture and technical refactor
	- DDD CQRS architecture pattern implementation: https://docs.microsoft.com/en-us/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/ddd-oriented-microservice
	- Mediator with MediatR: https://github.com/jbogard/MediatR
	- Autofac for components registration IoC, and easy Injection of parameter constructors: https://autofac.org/ 
	- Entity Framework 6 was implemented in order to avoid using a file as a data storage. However, Users.txt file still can be used to add several users reading from it. Code First approach is used.
	- Swagger definitions are inferred from the code and comments in controller's methods.
	- All projects have been migrated to NET 6 to achieve zero obsolescence.

### Code refactors
	- All bussines logic was removed from UserController.
	- CQRS pattern commands were created.
	- Base repository and User Repository were created.
	- Strategy design pattern is used to calculate gift amount based in user type.

### Tests
	- Unit and integration test has been added.	

### Usage:
	- The IDE used is Visual Studio 2022
	- Run API project will create (if does not exist) an SQLServer localdb database named "recruitment_db". Then, will open a page with the swagger definitions of the API.
	- In swagger there are two endpoints for User's controller definition:
		1.- add: allows to add a user to database.
		2.- add-from-file: allows to read an absolute path to a file (Users.txt) and process that file. Example: C:\Users.txt. 
		    This will add the 2 first users and will fail when process the 3rd.
		- To try all endpoints besides the other parameters always must complete api-version field with 1.
	- Running integration tests in test project will create (if does not exist) an SQLServer localdb database named "recruitment_db".
	- Database recruitment_db must appear in SQL Server Objects Explorer
	
### Technical debt:
	- Create an entity UserType related to User (with EF migration a table UserTypes related by an FK to Users table).
	- File validations (file exists, file type, file format, etc.).
	- Don't stop processing the file if a user is already added to the database.
	- Add more unit and integration tests.
	- Make async/awaitable GetFiltered in UserRepository.
	- Improve error messages.
	
