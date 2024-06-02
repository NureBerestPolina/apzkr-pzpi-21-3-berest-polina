using PickupCounterIoT.Services;
using PickupCounterIoT.Settings;
using System.Text.Json;

CounterSettings settings = new CounterSettings(String.Empty, default, default, default, default);

int doorOpenCount;
double minCellTemp;
double maxCellTemp;

Locale userLocale = GetUserLocale();
var resourceManager = LoadLocalizationResource(userLocale);

Console.Write(resourceManager["EnterDoorOpenCount"]);
if (int.TryParse(Console.ReadLine(), out doorOpenCount))
{
    Console.Write(resourceManager["EnterMinTemperature"]);
    if (double.TryParse(Console.ReadLine(), out minCellTemp))
    {
        Console.Write(resourceManager["EnterMaxTemperature"]);
        if (double.TryParse(Console.ReadLine(), out maxCellTemp))
        {
            Console.Write(resourceManager["EnterId"]);
            var id = Console.ReadLine();
            if (id?.Length == new Guid().ToString().Length)
            {
                settings = new CounterSettings(id, userLocale, doorOpenCount, minCellTemp, maxCellTemp);
                Console.WriteLine(resourceManager["Settings"], settings);
            }
            else
            {
                Console.WriteLine(resourceManager["InvalidInputId"]);
            }
        }
        else
        {
            Console.WriteLine(resourceManager["InvalidInputMaxTemperature"]);
        }
    }
    else
    {
        Console.WriteLine(resourceManager["InvalidInputMinTemperature"]);
    }
}
else
{
    Console.WriteLine(resourceManager["InvalidInputDoorOpenCount"]);
}


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddHttpClient();
var countStorage = builder.Configuration.GetValue<string>("FileStorages:OpenCountStorage");

const string CORS_POLICY = "CorsPolicy";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: CORS_POLICY,
        corsPolicyBuilder =>
        {
            corsPolicyBuilder.WithOrigins("http://localhost:5106");
            corsPolicyBuilder.AllowAnyMethod();
            corsPolicyBuilder.AllowAnyHeader();
        });
});

builder.Services.AddSingleton(settings);
builder.Services.AddHostedService<TemperatureService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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


static Locale GetUserLocale()
{
    while (true)
    {
        Console.Write("Select your language (en/ua): ");
        string selectedLocale = Console.ReadLine().ToLower();

        
        if (selectedLocale == "en" || selectedLocale == "ua")
        {
            return selectedLocale == "en" ? Locale.English : Locale.Ukrainian;
        }

        Console.WriteLine("Invalid choice. Please enter 'en' for English or 'ua' for Ukrainian.");
    }
}

static Dictionary<string, string> LoadLocalizationResource(Locale locale)
{
    string localizationFileName = $"Locales\\{locale}.json";
    Dictionary<string, string> resource = new Dictionary<string, string>();

    try
    {
        string json = File.ReadAllText(localizationFileName);
        resource = JsonSerializer.Deserialize<Dictionary<string, string>>(json);
    }
    catch (FileNotFoundException)
    {
        Console.WriteLine($"Localization file '{localizationFileName}' not found. Using default locale.");
        string defaultLocalizationFileName = "en.json";
        string json = File.ReadAllText(defaultLocalizationFileName);
        resource = JsonSerializer.Deserialize<Dictionary<string, string>>(json);
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error loading localization: {ex.Message}");
    }

    return resource;
}