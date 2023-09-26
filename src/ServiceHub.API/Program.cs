using ServiceHub.Core.Logger;
using ServiceHub.API.Application.Services.Profile;
using ServiceHub.API.Application.Services.Management;
using ServiceHub.Core.HostedServices.BackgroundServices;
using ServiceHub.Core.HostedServices.ServiceTypes.QueueService;
using ServiceHub.Core.Application.Models.FeatureControl;
using ServiceHub.Core.Application.Feature.Control;

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
