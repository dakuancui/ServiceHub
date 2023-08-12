using ServiceHub.API.Application.Features.Services;
using ServiceHub.ServiceEngine.HostedServices;
using ServiceHub.ServiceEngine.ServiceTypes.Periodic;
using ServiceHub.ServiceEngine.ServiceTypes.Scoped;
using ServiceHub.ServiceEngine.ServiceTypes.Singleton;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<ISingletonService, SingletonExampleService>();
builder.Services.AddHostedService<SingletonBackgroundService>();

builder.Services.AddScoped<IScopedService, ScopedExampleService>();
builder.Services.AddHostedService<ScopedBackgroundService>();

builder.Services.AddScoped<IPeriodicService, PeriodicExampleService>();
builder.Services.AddSingleton<PeriodicBackgroundService>();
builder.Services.AddHostedService(provider => provider.GetRequiredService<PeriodicBackgroundService>());


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

app.MapGet("/background", (
    PeriodicBackgroundService service) =>
{
    return new PeriodicHostedServiceState(service.IsEnabled);
});
app.MapMethods("/background", new[] { "PATCH" }, 
(
    PeriodicHostedServiceState state,
    PeriodicBackgroundService service) =>
{
    service.IsEnabled = state.IsEnabled;
});

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
