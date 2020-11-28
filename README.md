# Yo-Yo-Test
The Application is developed in .Net Core 3.1 Framework with MVC and RazorPages,
following a Multitier architecture.
• The application is compatible with both Web and mobile devices.
• The application is divided into 8 layers strictly following dependency injection and SOLID
  principles.
• Exception handling is done globally.
• Logging is implemented using Serilog and all the exceptions and logs are written to log.text
  file.
• AutoMapper is used for mapping the Entity to Model.
• Unit Test is written for BAL layer using xUnit framework.
• Dapper Repository is added as an ORM in DataAccess layer.
• RegisterServices is separated from Startup.cs and created as a separate class library to avoid
  tightly coupled project.
• Proper commenting, regions are added for better readability of code.
• UI design is done using bootstrap and custom CSS. Flux design is used.
• Client side scripting is done in JavaScript/jQuery libraries.
• Partial Views are used for Displaying the Athletes list and Fitness Test Data.
• Toaster is added if user warn/stop an athlete.

