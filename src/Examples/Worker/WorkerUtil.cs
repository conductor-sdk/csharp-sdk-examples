using Conductor.Client.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Examples.Worker
{
    public class WorkerUtil
    {
        public static IHost GetWorkerHost()
        {
            return new HostBuilder()
                .ConfigureServices(
                    (ctx, services) =>
                        {
                            services.AddConductorWorker(Examples.Api.ApiUtil.GetConfiguration());
                            services.AddConductorWorkflowTask<SimpleWorker>();
                            services.WithHostedService<WorkerService>();
                        }
                ).ConfigureLogging(
                    logging =>
                        {
                            logging.SetMinimumLevel(LogLevel.Debug);
                            logging.AddConsole();
                        }
                ).Build();
        }

        public static void StartWorkers()
        {
            IHost host = WorkerUtil.GetWorkerHost();
            host.RunAsync();
        }
    }
}