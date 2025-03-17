using System.Text.Json.Serialization;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using qwitix_api.Core.Repositories;
using qwitix_api.Core.Repositories.EventRepository;
using qwitix_api.Core.Services.EventService;
using qwitix_api.Core.Services.OrganizerService;
using qwitix_api.Core.Services.TicketService;
using qwitix_api.Core.Services.TransactionService;
using qwitix_api.Core.Services.UserService;
using qwitix_api.Infrastructure.Configs;
using qwitix_api.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

ConfigureServices(builder.Services, builder.Configuration);

var app = builder.Build();

ConfigureMiddleware(app);

app.Run();

// Setting Service
void ConfigureServices(IServiceCollection services, IConfiguration configuration)
{
    services.Configure<DatabaseSettings>(configuration.GetSection(nameof(DatabaseSettings)));

    services.AddScoped<IUserRepository, UserRepository>();
    services.AddScoped<UserService>();

    services.AddScoped<IOrganizerRepository, OrganizerRepository>();
    services.AddScoped<OrganizerService>();

    services.AddScoped<ITicketRepository, TicketRepository>();
    services.AddScoped<TicketService>();

    services.AddScoped<IEventRepository, EventRepository>();
    services.AddScoped<EventService>();

    services.AddScoped<ITransactionRepository, TransactionRepository>();
    services.AddScoped<TransactionService>();

    services.AddControllers();

    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen();

    services.Configure<JsonOptions>(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });
}

// Setting middleware
void ConfigureMiddleware(WebApplication app)
{
    if (app.Environment.IsDevelopment())
        ConfigureSwagger(app);

    app.UseForwardedHeaders(
        new ForwardedHeadersOptions
        {
            ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto,
        }
    );

    app.UseAuthorization();
    app.MapControllers();
}

// Setting Swagger
void ConfigureSwagger(WebApplication app)
{
    app.UseSwagger(c =>
    {
        c.RouteTemplate = "api/swagger/{documentname}/swagger.json";
    });

    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/api/swagger/v1/swagger.json", "API V1");
        c.RoutePrefix = "api/swagger";
    });
}
