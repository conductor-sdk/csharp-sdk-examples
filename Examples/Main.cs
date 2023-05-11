using Conductor.Client.Extensions;
using System.Threading.Tasks;
using System;

var host = WorkflowTaskHost.CreateWorkerHost(
    ApiExtensions.GetConfiguration(),
    Microsoft.Extensions.Logging.LogLevel.Debug
);

await host.StartAsync();
await Task.Delay(TimeSpan.FromSeconds(5));
await host.StopAsync();
