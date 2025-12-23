using BikeRental.Application.Contracts.RentalRecords;
using BikeRental.Generator.Kafka.Host.Configuration;
using BikeRental.Generator.Kafka.Host.Generation;
using BikeRental.Generator.Kafka.Host.Producing;
using BikeRental.Generator.Kafka.Host.Serialization;
using BikeRental.Generator.Kafka.Host.Workers;
using Confluent.Kafka;

var builder = Host.CreateApplicationBuilder(args);

builder.AddServiceDefaults();

builder.Services.Configure<KafkaProducerOptions>(builder.Configuration.GetSection(KafkaProducerOptions.SectionName));
builder.Services.Configure<GeneratorOptions>(builder.Configuration.GetSection(GeneratorOptions.SectionName));

builder.Services.AddSingleton(sp =>
{
    var connectionString = builder.Configuration.GetConnectionString("kafka");

    var config = new ProducerConfig
    {
        BootstrapServers = connectionString,
        Acks = Acks.All,
        EnableIdempotence = true
    };

    return new ProducerBuilder<int, RentalRecordCreateUpdateDto>(config)
        .SetKeySerializer(new JsonKeySerializer<int>())
        .SetValueSerializer(new JsonValueSerializer<RentalRecordCreateUpdateDto>())
        .Build();
});

builder.Services.AddSingleton<RentalRecordGenerator>();
builder.Services.AddSingleton<RentalRecordProducer>();

builder.Services.AddHostedService<GeneratorWorker>();

var host = builder.Build();
host.Run();