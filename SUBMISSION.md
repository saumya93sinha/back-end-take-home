Submission Instructions

Technologies Used:-
1. Web API using Asp.Net MVC Web API in C# .Net
2. Angular 7
3. MS SQL SERVER
4. Typescript
5. Unit Testcases for Web API

-------------------------------------------------------------------------------------------------

Pre Requisites to run the application:
1. Install Visual Studio 2015
2. Install Visual Studio Code
3. Install Angular 7 as follows:
	i) Please visit https://angular.io/guide/quickstart to know steps how to install Angular
	   Before you begin, make sure your development environment includes Node.js® and an npm package manager.
       
	   Node.js
	   Angular requires Node.js version 8.x or 10.x.
       
       To check your version, run node -v in a terminal/console window.
       
       To get Node.js, go to nodejs.org.
	   
	ii) Install the Angular CLI
		Install the Angular CLI globally.

		To install the CLI using npm, open a terminal/console window and enter the following command:

		npm install -g @angular/cli

-------------------------------------------------------------------------------------------------------------

Instructions to run the application:		
1. Clone or Download the solution from Github
2. You will see the following folders:
	i)GuestlogixAPI - This is the Web API solution
	ii)GuestlogixUI - This is the Angular Application
	iii)data - Contains airlines, airport and routes csvs
3. Open GuestlogixAPI solution in Visual Studio 2015
   i) Right click on the project Guestlogix.WebAPI and click on set as start up project
   ii) Build the solution and run on Google Chrome
   iii) The API's URL is http://localhost:54243
4. Open the GuestlogixUI folder on Visual Studio Code
   i) Open command prompt and set the location to GuestlogixUI folder
   ii) Make sure http://localhost:4200/ is not open on your browser
   iii) Type the commands:
		a) npm install
		b) npm install -g @angular/cli
		c) ng serve
   iii) This will start building the Angular Application.
   iv) Once the build is succeeded , open the url http://localhost:4200/
   v) http://localhost:4200/ is the URL for Angular Application which contains the front end UI of the application
   vi) Make sure that both the Web API and Angular applications are up and running.
5. Application is launched and can be tested.
6. To run the unit tests:
   i) Go to the GuestlogixAPI solution in Visual Studio 2015
   ii) Click on Test from the Menu
   iii) Go to Run -> All Tests
   iv) You can view the test results in Test Explorer Window

-----------------------------------------------------------------------------------------------------------------------------
   
Explanation of Code:
1. GuestlogixAPI -- Backend Application
	i) Guestlogix.WebAPI 
	This is the WebAPI project and RouteController.cs is the controller where API methods are present.
	We have two API methods:
		a)http://localhost:54243/route/getshortestroute - This is to get the shortest route
		It requires one object of RouteSearchParam class(created by me) as param. 
		From Postman we can hit this web API by setting header as Content-Type = 'application/json'
		and raw body data like
		{
       "Origin": "ABJ",
        "Destination": "BRU"        
		}
		It will return the shortest route as string between origin and destination
	
		b)http://localhost:54243/route/getairports - This will fetch list of all the airports.
		I am using this to populate origin and destination drop-downs on UI.
		
	ii) Guestlogix.Business
	This is the business layer of the application
	
	iii) Guestlogix.Models
	This contains all the model classes used in the application
	
	iv) Guestlogix.Data
	This the repository layer where actually calculation of shortest path takes place.
	Currently I am directly populating lists of airports, airlines and routes from the CSVs only.
	I am not using SQL tables as of now. But if I would have used SQL for database as well, then repository
	in Guestlogix.Data would be fetching data from SQL Server's database.
	
	v) Guestlogix.WebAPI.Tests
	This is used for writing test cases for methods of RouteRepository and HomeController.
	
	vi) Guestlogix.DB
	Although I am not using MS SQL Server for this application, I have designed 
	the table structures and their dependencies, primary key, foreign key constraints and indexes.
	Please have a look at it!
	
2. GuestlogixUI -- Frontend Framework
    This has components, services, models, modules, routing modules.
	Typescript, HTML and CSS are used in creating this Angular Application.
	
Proper comments have been placed in the code for better code understanding and maintainability.
I have also attached screenshots of WebAPI and Application main UI page for reference.

Thanks!
It was a pleasure working on this application!