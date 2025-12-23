using BikeRental.Application;
using BikeRental.Application.Contracts;
using BikeRental.Application.Contracts.BikeModels;
using BikeRental.Application.Contracts.Bikes;
using BikeRental.Application.Contracts.RentalRecords;
using BikeRental.Application.Contracts.Renters;
using BikeRental.Application.Services;
using BikeRental.Domain;
using BikeRental.Domain.Data;
using BikeRental.Domain.Models;
using BikeRental.Infrastructure.EfCore;
using BikeRental.Infrastructure.EfCore.Repositories;
using BikeRental.Infrastructure.Kafka.Configuration;
using BikeRental.Infrastructure.Kafka.Consumers;
using BikeRental.Infrastructure.Kafka.Serialization;
using Confluent.Kafka;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<DataSeeder>();

builder.AddServiceDefaults();

builder.Services.AddAutoMapper(config =>
{
    config.AddProfile(new ApplicationMappingProfile());
});

builder.Services.AddScoped<IBikeService, BikeService>();
builder.Services.AddScoped<IRentalRecordService, RentalRecordService>();
builder.Services.AddScoped<IApplicationService<BikeModelDto, BikeModelCreateUpdateDto, int>, BikeModelService>();
builder.Services.AddScoped<IApplicationService<RenterDto, RenterCreateUpdateDto, int>, RenterService>();
builder.Services.AddScoped<IAnalyticsService, AnalyticsService>();
builder.Services.AddTransient<IRepository<Bike, int>, BikeRepository>();
builder.Services.AddTransient<IRepository<BikeModel, int>, BikeModelRepository>();
builder.Services.AddTransient<IRepository<RentalRecord, int>, RentalRecordRepository>();
builder.Services.AddTransient<IRepository<Renter, int>, RenterRepository>();

builder.AddSqlServerDbContext<BikeRentalDbContext>("Database");

builder.Services.Configure<KafkaConsumerOptions>(builder.Configuration.GetSection(KafkaConsumerOptions.SectionName));

builder.Services.AddSingleton(sp =>
{
    var options = sp.GetRequiredService<IOptions<KafkaConsumerOptions>>().Value;
    var connectionString = builder.Configuration.GetConnectionString("kafka");

    var config = new ConsumerConfig
    {
        BootstrapServers = connectionString,
        GroupId = options.GroupId,
        AutoOffsetReset = AutoOffsetReset.Earliest,
        EnableAutoCommit = false
    };

    return new ConsumerBuilder<int, RentalRecordCreateUpdateDto>(config)
        .SetKeyDeserializer(new JsonKeyDeserializer<int>())
        .SetValueDeserializer(new JsonValueDeserializer<RentalRecordCreateUpdateDto>())
        .Build();
});

builder.Services.AddHostedService<RentalRecordConsumerService>();

builder.Services.AddControllers().AddJsonOptions(o =>
{
    o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.UseInlineDefinitionsForEnums();

    var assemblies = AppDomain.CurrentDomain.GetAssemblies()
        .Where(a => a.GetName().Name!.StartsWith("BikeRental"))
        .Distinct();

    foreach (var assembly in assemblies)
    {
        var xmlFile = $"{assembly.GetName().Name}.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
        if (File.Exists(xmlPath))
            c.IncludeXmlComments(xmlPath);
    }

});

var app = builder.Build();

app.MapDefaultEndpoints();

if (app.Environment.IsDevelopment())
{
    using var scope = app.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<BikeRentalDbContext>();
    await db.Database.MigrateAsync();

    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();