using System.Text;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using qwitix_api.Core.Processors;
using qwitix_api.Core.Repositories;
using qwitix_api.Core.Repositories.EventRepository;
using qwitix_api.Core.Services.AccountService;
using qwitix_api.Core.Services.EventService;
using qwitix_api.Core.Services.OrganizerService;
using qwitix_api.Core.Services.TicketService;
using qwitix_api.Core.Services.TransactionService;
using qwitix_api.Core.Services.UserService;
using qwitix_api.Infrastructure.Configs;
using qwitix_api.Infrastructure.Handlers;
using qwitix_api.Infrastructure.Processors;
using qwitix_api.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Configs
builder.Services.Configure<DatabaseSettings>(
    builder.Configuration.GetSection(nameof(DatabaseSettings))
);
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection(nameof(JwtSettings)));

// Cors
builder.Services.AddCors(opt =>
{
    opt.AddPolicy(
        "CorsPolicy",
        options =>
        {
            options
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials()
                .WithOrigins("http://localhost:5173");
        }
    );
});

// Services
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<UserService>();

builder.Services.AddScoped<IOrganizerRepository, OrganizerRepository>();
builder.Services.AddScoped<OrganizerService>();

builder.Services.AddScoped<ITicketRepository, TicketRepository>();
builder.Services.AddScoped<TicketService>();

builder.Services.AddScoped<IEventRepository, EventRepository>();
builder.Services.AddScoped<EventService>();

builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();
builder.Services.AddScoped<TransactionService>();

builder.Services.AddScoped<IAuthTokenProcessor, AuthTokenProcessor>();
builder.Services.AddScoped<AccountService>();

// Authentication
builder
    .Services.AddAuthentication(opt =>
    {
        opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        opt.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddCookie()
    .AddGoogle(options =>
    {
        var clientId = builder.Configuration["Authentication:Google:ClientId"];

        if (clientId == null)
        {
            throw new ArgumentNullException(nameof(clientId));
        }

        var clientSecret = builder.Configuration["Authentication:Google:ClientSecret"];

        if (clientSecret == null)
        {
            throw new ArgumentNullException(nameof(clientSecret));
        }

        options.ClientId = clientId;
        options.ClientSecret = clientSecret;
        options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;

        //options.Events.OnRemoteFailure = context =>
        //{
        //    context.Response.Redirect("/login?error=GoogleLoginFailed");
        //    context.HandleResponse();
        //    return Task.CompletedTask;
        //};
    })
    .AddJwtBearer(options =>
    {
        var jwtSettings =
            builder.Configuration.GetSection(nameof(JwtSettings)).Get<JwtSettings>()
            ?? throw new ArgumentException(nameof(JwtSettings));
        ;

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSettings.Issuer,
            ValidAudience = jwtSettings.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret)),
        };

        options.Events = new JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                context.Token = context.Request.Cookies["ACCESS_TOKEN"];
                return Task.CompletedTask;
            },
        };
    });

// Controlles
builder.Services.AddControllers();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<JsonOptions>(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

// Authorization
builder.Services.AddAuthorization();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

builder.Services.AddHttpContextAccessor();

var app = builder.Build();

app.UseCors("CorsPolicy");

app.UseExceptionHandler(_ => { });
app.UseAuthorization();

if (app.Environment.IsDevelopment())
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
app.UseForwardedHeaders(
    new ForwardedHeadersOptions
    {
        ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto,
    }
);

app.MapControllers();

app.Run();
