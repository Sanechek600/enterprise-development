var builder = DistributedApplication.CreateBuilder(args);

var sqlServer = builder.AddSqlServer("bikerental-sql-server")
                 .AddDatabase("BikeRentalDb");

builder.AddProject<Projects.BikeRental_Api_Host>("bikerental-api-host")
       .WithReference(sqlServer, "Database")
       .WaitFor(sqlServer);

builder.Build().Run();