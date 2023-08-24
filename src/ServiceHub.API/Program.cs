using ServiceHub.API.Application.Features;
using ServiceHub.API.Application.Services;
using ServiceHub.API.Application.Models;
using ServiceHub.API.Logger;
using ServiceHub.ServiceEngine.HostedServices;
using ServiceHub.ServiceEngine.ServiceTypes.Periodic;
using ServiceHub.ServiceEngine.ServiceTypes.Scoped;
using ServiceHub.API.Application.Triggers;
using ServiceHub.API.Application.Consumers;
using ServiceHub.API.Application.Models.FeatureConfigurations;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddSingleton<IFeature, HealthLinkInterfaceFeature<HealthLinkInterfaceConfiguration>>();
builder.Services.AddHostedService<HealthLinkInterfaceService<IFeature>>();

//builder.Services.AddScoped<IScopedService, ScopedExampleService>();
//builder.Services.AddHostedService<ScopedBackgroundService>();

//builder.Services.AddScoped<IPeriodicService, PeriodicExampleService>();
//builder.Services.AddSingleton<PeriodicBackgroundService>();
//builder.Services.AddHostedService(provider => provider.GetRequiredService<PeriodicBackgroundService>());


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.Logging.AddJsonConsole(options =>
//{
//    options.IncludeScopes = false;
//    options.TimestampFormat = "HH:mm:ss ";
//    options.JsonWriterOptions = new JsonWriterOptions
//    {
//        Indented = true
//    };
//});
var app = builder.Build();

var loggerFactory = app.Services.GetService<ILoggerFactory>();
var logRootPath = @"/Users/DakuanC/Dakuan.asb/spikes/ServiceHub/src";
loggerFactory?.AddLogRootPath(logRootPath);

//app.MapGet("/background", (
//    PeriodicBackgroundService service) =>
//{
//    return new PeriodicHostedServiceState(service.IsEnabled);
//});
//app.MapMethods("/background", new[] { "PATCH" }, 
//(
//    PeriodicHostedServiceState state,
//    PeriodicBackgroundService service) =>
//{
//    service.IsEnabled = state.IsEnabled;
//});

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
