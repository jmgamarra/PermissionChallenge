using Elasticsearch.Net;
using Microsoft.EntityFrameworkCore;
using Nest;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PermissionsWeb.Application.Services;

var builder = WebApplication.CreateBuilder(args);
//builder.WebHost.UseUrls("http://*:80");
// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("N5Connection");

// Configurar Elasticsearch
var elasticsearchSettings = builder.Configuration.GetSection("Elasticsearch");
var url = elasticsearchSettings["Url"];
var index = elasticsearchSettings["Index"];

var settings = new ConnectionSettings(new Uri(url))
     .DefaultIndex(index)
    .ServerCertificateValidationCallback(CertificateValidations.AllowAll)
    .DisableDirectStreaming();

var client = new ElasticClient(settings);

builder.Services.AddSingleton<IElasticClient>(client);

// Add services to the container.
builder.Services.AddDbContext<PermissionsDbContext>(options =>
            options.UseSqlServer(connectionString));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add DI
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IPermisoRepository, PermisoRepository>();

// Registrar MediatR y los manejadores de CQRS
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(RequestPermisoCommandHandler).Assembly));

var app = builder.Build();

// Aplicar migraciones de base de datos
ApplyDatabaseMigrations(app);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

void ApplyDatabaseMigrations(IHost app)
{
    using (var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;
        try
        {
            var context = services.GetRequiredService<PermissionsDbContext>();
            context.Database.Migrate();
        }
        catch (Exception ex)
        {
            var logger = services.GetRequiredService<ILogger<Program>>();
            logger.LogError(ex, "An error occurred while migrating the database.");
        }
    }
}
 void ConfigureServices(IServiceCollection services)
{
    // Otras configuraciones

    services.AddSingleton<KafkaProducerService>(sp =>
    {
        var configuration = sp.GetRequiredService<IConfiguration>();
        var bootstrapServers = configuration["Kafka:BootstrapServers"];
        return new KafkaProducerService(bootstrapServers);
    });

    // Otras configuraciones
}
