using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        var connectionString = builder.Configuration.GetConnectionString("N5Connection");

        // Configurar Elasticsearch
        //var elasticsearchSettings = builder.Configuration.GetSection("Elasticsearch");
        //var url = elasticsearchSettings["Url"];
        //var index = elasticsearchSettings["Index"];

        //var settings = new ConnectionSettings(new Uri(url))
        //    .DefaultIndex(index);

        //var client = new ElasticClient(settings);

        //builder.Services.AddSingleton<IElasticClient>(client);

        // Add services to the container.
        builder.Services.AddDbContext<PermissionsDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("N5Connection")));

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
