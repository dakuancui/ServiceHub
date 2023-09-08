using ServiceHub.API.Application.Models.FeatureControl;
using ServiceHub.API.Logger;
using ServiceHub.ServiceEngine.HostedServices;
using ServiceHub.ServiceEngine.ServiceTypes.QueueService;
using ServiceHub.API.Application.Services.Profile;
using ServiceHub.API.Application.Services.Management;
using ServiceHub.API.Application.Providers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IProfileService, ProfileService>();
builder.Services.AddSingleton<IManagementService, ManagementService>();
builder.Services.AddSingleton<IObservable<FeatureCommand>, FeatureCommandPublisher>();
builder.Services.AddHostedService<QueuedHostedService>();

builder.Services.AddSingleton<IQueueTaskService>(_=>
{
    return new QueueTaskService(100);
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

var loggerFactory = app.Services.GetService<ILoggerFactory>();
var logRootPath = @"/Users/DakuanC/Dakuan.asb/spikes/ServiceHub/src";
loggerFactory?.AddLogRootPath(logRootPath);

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
