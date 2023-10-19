using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using TestesMicroservicoDocker;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddDbContext<ApplicationContext>(options =>
    {
        var connectionStrings = Environment.GetEnvironmentVariable("ConnectionStrings__Postgresql");
        if (connectionStrings == null) throw new ArgumentNullException("ConnectionStrings__Postgresql");
        options.UseNpgsql(connectionStrings);
    });

builder.Services
    .AddSingleton<IConnectionMultiplexer>(options =>
    {
        var connectionString = Environment.GetEnvironmentVariable("ConnectionStrings__Redis");
        var connectionMultiplexer = ConnectionMultiplexer.Connect(connectionString);
        return connectionMultiplexer;
    });

builder.Services
    .AddAutoMapper(config => config.AddProfile<AutoMapperProfile>());

builder.Services
    .AddApiVersioning(options =>
    {
        options.DefaultApiVersion = new ApiVersion(1, 0);
        options.AssumeDefaultVersionWhenUnspecified = true;
        options.ReportApiVersions = true;
        options.ApiVersionReader = ApiVersionReader.Combine(
            new UrlSegmentApiVersionReader(),
            new HeaderApiVersionReader("x-api-version"),
            new MediaTypeApiVersionReader("x-api-version")
        );
    });
builder.Services
    .AddVersionedApiExplorer(
        options =>
        {
            options.GroupNameFormat = "'v'VVV";
            options.SubstituteApiVersionInUrl = true;
        });

builder.Services.AddControllers();
builder.Services.AddHealthChecks();
builder.Services.AddHttpContextAccessor();
builder.Services.AddAuthorization();
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
builder.Services.AddHttpsRedirection(options => options.RedirectStatusCode = StatusCodes.Status307TemporaryRedirect);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services
    .ConfigureHttpJsonOptions(options =>
    {
        options.SerializerOptions.PropertyNamingPolicy = null;
        options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });

var app = builder.Build();

app.MapGet("/", () => "Hello World!");
app.MapGet("/pessoas", ([FromServices] ApplicationContext context) =>
{
    var result = context.Pessoas.ToList();
    return Results.Ok(result);
});
app.MapGet("/configs", (
    [FromServices] IConnectionMultiplexer redis
) =>
{
    var _database = redis.GetDatabase();
    var cachedData = _database.StringGet("Configs");

    var configs = new Config();
    if (cachedData.HasValue)
    {
        configs = JsonSerializer.Deserialize<Config>(cachedData);
    }
    else
    {
        configs = new Config
        {
            GeneratedAt = DateTime.Now
        };

        _database.StringSet("Configs", JsonSerializer.Serialize(configs), new TimeSpan(0, 2, 0));
    }
    
    return configs;
});

app.Run();
