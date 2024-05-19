using Microsoft.AspNetCore.Identity;
using Microsoft.OpenApi.Models;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using FluentValidation;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using Microsoft.AspNetCore.OData;
using EnRoute.Domain.Models;
using EnRoute.Domain;
using EnRoute.API.Middleware;
using Microsoft.EntityFrameworkCore;
using EnRoute.Common.Configuration;
using EnRoute.Infrastructure.Services.Interfaces;
using EnRoute.Infrastructure.Services;
using EnRoute.API.Contracts.Auth.Requests;
using EnRoute.API.Contracts.DomainValidators;
using EnRoute.Infrastructure.Strategies;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddUserSecrets(typeof(Program).Assembly);

JwtSettings jwtSettings = builder.Configuration.GetSection("JwtSettings").Get<JwtSettings>() ??
                          throw new Exception("JWT configuration is missing");

AdminSettings adminSettings = builder.Configuration.GetSection("AdminSettings").Get<AdminSettings>() ??
                              throw new Exception("Admin configuration is missing");

SysadminSettings sysadminSettings = builder.Configuration.GetSection("SysadminSettings").Get<SysadminSettings>() ??
                              throw new Exception("Sustem Administrator configuration is missing");

CellConnectionSettings cellConnectionSettings = builder.Configuration.GetSection("CellConnectionSettings").Get<CellConnectionSettings>() ??
                              throw new Exception("IoT devices (Counter Cells) connection configuration is missing");

builder.Services.AddSingleton(jwtSettings);
builder.Services.AddSingleton(adminSettings);
builder.Services.AddSingleton(sysadminSettings);
builder.Services.AddSingleton(cellConnectionSettings);
builder.Services.AddSingleton<IJwtTokenService, JwtTokenService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ICounterService, CounterService>();
builder.Services.AddScoped<IStatisticsService, StatisticsService>();
builder.Services.AddScoped<IDeliveryService, DeliveryService>();
builder.Services.AddScoped<IRoleStrategyFactory, RoleStrategyFactory>();

builder.Services.AddHttpClient();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MsSql"),
        o => o.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

builder.Services.AddIdentity<User, IdentityRole<Guid>>(o =>
{
    o.Password.RequireDigit = false;
    o.Password.RequireLowercase = false;
    o.Password.RequireNonAlphanumeric = false;
    o.Password.RequireUppercase = false;
    o.Password.RequiredLength = 6;
    o.Password.RequiredUniqueChars = 0;
})
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
    {
        options.SaveToken = true;
        options.RequireHttpsMetadata = false;

        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ClockSkew = TimeSpan.Zero,

            ValidAudience = jwtSettings.ValidAudience,
            ValidIssuer = jwtSettings.ValidIssuer,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret))
        };
    });

const string CORS_POLICY = "CorsPolicy";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: CORS_POLICY,
        corsPolicyBuilder =>
        {
            corsPolicyBuilder.AllowAnyOrigin();
            corsPolicyBuilder.AllowAnyMethod();
            corsPolicyBuilder.AllowAnyHeader();
        });
});

builder.Services.AddControllers(c =>
{
    c.ModelValidatorProviders.Clear();
    c.ValidateComplexTypesIfChildValidationFails = false;
}).AddOData(opt =>
{
    opt.AddRouteComponents("odata", GetEdmModel());
}).AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.JsonSerializerOptions.WriteIndented = true;
});

builder.Services.AddAutoMapper(typeof(EnRoute.API.MappingProfile).Assembly);

builder.Services.AddValidatorsFromAssemblyContaining<LoginRequestValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<TechInspectionRequestValidator>();
builder.Services.AddFluentValidationAutoValidation();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      \r\n\r\nExample: 'Bearer 12345abcdef'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header

            },
            new List<string>()
        }
    });
});

builder.Services.AddHostedService<AdminInitializerHostedService>();
builder.Services.AddHostedService<SysadminInitializerHostedService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(CORS_POLICY);

//app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseExceptionHandlerMiddleware();

app.MapControllers();


//app.UseSpa(c => c.UseProxyToSpaDevelopmentServer("http://localhost:4200/"));

app.Run();


static IEdmModel GetEdmModel()
{
    var builder = new ODataConventionModelBuilder();
    builder.EntitySet<User>("Users").EntityType.Count().Filter().Expand().Select();
    builder.EntitySet<Organization>("Organizations").EntityType.Count().Filter().Expand().Select();
    builder.EntitySet<Order>("Orders").EntityType.Count().Filter().Expand().Select();
    builder.EntitySet<OrderItem>("OrderItems").EntityType.Count().Filter().Expand().Select();
    builder.EntitySet<Good>("Goods").EntityType.Count().Filter().Expand().Select();
    builder.EntitySet<Producer>("Producers").EntityType.Count().Filter().Expand().Select();
    builder.EntitySet<Category>("Categories").EntityType.Count().Filter().Expand().Select();
    builder.EntitySet<TechInspectionRequest>("TechInspectionRequests").EntityType.Count().Filter().Expand().Select();
    builder.EntitySet<PickupCounter>("PickupCounters").EntityType.Count().Filter().Expand().Select();
    builder.EntitySet<CounterInstallationRequest>("CounterInstallationRequests").EntityType.Count().Filter().Expand().Select();
    builder.EntitySet<CounterDeinstallationRequest>("CounterDeinstallationRequests").EntityType.Count().Filter().Expand().Select();

    builder.EnableLowerCamelCase();
    return builder.GetEdmModel();
}
