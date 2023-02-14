# Sundown
This application provides a CRUD API in order to manage users, mission reports, and mission images. It also provides a command that, when executed, it emits an event that contain the facility and the time when an astronaut should land.

## Running
In order to run the application, one can use docker compose, by running the following command from the root folder:
```
docker-compose up
```
After the dockers are up and running, one can access the endpoint by using the following base API URL `http://localhost`. The https certificates were not defined. Because all the endpoints are token protected, one must generate a token first. Check the [security](#security) section to see how that is done.
The docker compose also starts an instance of a [PostrgreSQL](https://www.postgresql.org/), which is populated with initial data as defined in the `MissionReportingTool/Migrations` folder.

## Running test
Unit tests have been defined for all services. These can be ran using the dotnet cli, by running the following command from the root folder:
```
dotnet test
```

## Stack
The application is built using the following:
- [.NET 6.0](https://dotnet.microsoft.com/en-us/download)
- [Entity Frmework Core](https://learn.microsoft.com/en-us/ef/core/)
- [npgsql](https://www.npgsql.org/efcore/)
- [Quartz](https://www.quartz-scheduler.net/)

## Security
For this application, JWT security was used. There is open endpoint defined `POST:/Api/Authenticate` that requires the following body:
```json
{
    "username": "username",
    "password": "username"
}
```
It provides the following response:
```json
{
    "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VyaWQiOiI3IiwidXNlcm5hbWUiOiJhZG1pbiIsImZpcnN0bmFtZSI6IkFkbWluIiwibGFzdG5hbWUiOiJBZG1pbnNlbiIsImNvZGVuYW1lIjoiU3VwZXJVc2VyIiwiYXZhdGFyIjoiaHR0cHM6Ly90aHVtYnMuZHJlYW1zdGltZS5jb20vYi9hZG1pbi1zaWduLWxhcHRvcC1pY29uLXN0b2NrLXZlY3Rvci0xNjYyMDU0MDQuanBnIiwicm9sZSI6IkFETUlOIiwianRpIjoiOGE2NzE5MDEtM2FmZi00OWY3LTg2YzItYmRjYzM2NWFhMWU3IiwiZXhwIjoxNjc2Mzg1OTMxLCJpc3MiOiJodHRwczovL21ydC5jb20iLCJhdWQiOiJodHRwczovL2lzcy5jb20ifQ.4hQ53ru0RK1BSyPEfO2m-hGhlTZLciMAcRfcgAMsadI",
    "validUntil": "2023-02-14T14:45:31Z"
}
```
The token must be used to access all the other endpoints of the application as a bearer token. There are also roles based security defined for each endpoint, that will be dsicussed in the [roles](#roles) section.

## Roles
The following Roles are defined:
- ASTRONAUT -> can read/update/delete its own user information, can change its own password, and can manipulate any mission report and mission image
- SCIENTIST -> can read/update/delete its own user information, can change its own password, and can read any mission report and mission image
- GENERAL -> can read/update/delete its own user information, can change its own password, and can execute the land command
- ADMIN -> can manipulate any user, and can execute the land command 

## Cron jobs
A cron job was defined using [Quartz](https://www.quartz-scheduler.net/). This cronjob calculates the closes facility from the ISS satellite, and saves this information into the database. The [wheretheiss API](https://wheretheiss.at/w/developer) was used to get the location of the sattelite.

The timing of this cron job can be defined in the `appsettings.json` or as an environment variable as follows:
```json
{
  "LandingJobCron": "0 0/5 * * * ?" /* run every 5 minutes */
}
```

## Commands
A user with the `GENERAL` role can call the `POST:/Api/Commands/Land` endpoint, to intiate the land command. This will calculated the best time to land in a 2 minute winow, based on the lower temperature (the [open meteo API](https://open-meteo.com/) was used to obtain the temperature forecast for the next two minutes), to the closest facility (which was saved by the [cron job](#cron-jobs)). This will emit a `LandedEvent` that is handeld in the `LandHandler` class, which logs the information of the event.

## Structure
The entitites which map to the database tabels are defined in the `MissionReportingTool/Entitites` folder. There is also defined an abstract class called `BaseEntity` that can be extended in order to create soft-delete-ready entities, and have id, createdAt, modifiedAt, and deletedAt fields.

The entities are manipulated by repositories which are defined in the `MissionReportingTool/Repositories` folder. An interfaces called `IRepository` is defined in the `MissionReportingTool/Repositories/Interfaces` folder, and can be used to create CRUD repository interfaces, as it comes with the basic CRUD functionality. This functionality is implemented in the `BaseRepository` abstract class, which uses safe deletion (only sets the deletedAt field, and then filtering the entitites out that has this field already set). The combination of these two can be used to create a new CRUD repository without much effort.

The repositories are then used in services, which contain all the business logic, and can be found in the `MissionReportingTool/Services` folder. A structure similar to the repositories, has been defined for the services. In the `MissionReportingTool/Services/Interfaces` folder, a `IService` interface has been defined, which comes with the basic CRUD functionality. This functionality is implemented in the `BaseService`,. which uses a repository. This service also maps the entitites to contract.

The services are then used in controller, defined in the `MissionReportingTool/Controllers` folder. A `BaseCrudController` abstract class has been defined, which comes will all the CRUD basic functionality, and can be extended to create a CRUD controller with ease.

The communication between the client and the server is done using contracts which are defined in the `MissionReportingTool/Contracts` folder. Here a `BaseContract` has been defined that can be used for the entities that have an id. In the `Contracts` folder, one can find contracts that can be used as both requests and responses. If a contract can be used only as request/response, these can be found under their respectivly folder. In the `Requests` folder, the `BaseCreationRequest` is defined, that needs to be extened to define creation requests, whcih will be used by services that extend the `BaseService`

Functionality that can be extracted to a library to be used in other applications, has been defined in classes with static methods in the `MissionReportingTool/Helpers` folder.

## ErrorHandling
A `BaseHttpResponseExceptionFilter` was defined that maps exceptions, which extend the `BaseHttpResponseException`, to HTTP responses with custom status codes. This way specific exceptions can be defined, and used in the services, while no extra work is necessary from the controllers.

## API documentation
A swagger page can be seen when running the application locally in debug mode by accessing the `/swagger/index.html` on the `locahost` with the specific port. The `swagger.json` file from the `Documentation` folder, can also be uploaded to https://editor.swagger.io/.

## Future improvements
1. Defining integration tests by using [test containers](https://dotnet.testcontainers.org/) for the database. This way the repositories and the controllers can be tested.
2. Using a proper messaging queue like [rabbitMq](https://www.rabbitmq.com/) or [kafka](https://kafka.apache.org/) in order to publish and handle the land event.
3. Separate the cron job, and the Commands API into different micro services, so they are not dependent on the main CRUD application.
4. Allow image uplod from the client for the mission images, which will then be saved in S3 bucket or something similar on a server, and then persisiting only the URL to the file in the database.