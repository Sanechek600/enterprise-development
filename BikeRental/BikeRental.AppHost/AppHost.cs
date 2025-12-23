var builder = DistributedApplication.CreateBuilder(args);

var sqlServer = builder.AddSqlServer("bikerental-sql-server")
                 .AddDatabase("BikeRentalDb");

var kafka = builder.AddKafka("kafka")
    .WithKafkaUI()
    .WithEnvironment("KAFKA_AUTO_CREATE_TOPICS_ENABLE", "true");

builder.AddProject<Projects.BikeRental_Api_Host>("bikerental-api-host")
       .WithReference(sqlServer, "Database")
       .WithReference(kafka)
       .WaitFor(sqlServer)
       .WaitFor(kafka);

builder.AddProject<Projects.BikeRental_Generator_Kafka_Host>("bikerental-generator")
       .WithReference(kafka)
       .WaitFor(kafka);

builder.Build().Run();