using Conductor.Client.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Examples.Worker
{
    public class WorkerUtil
    {
        public static void StartWorkers()
        {
            IHost host = GetWorkerHost();
            host.RunAsync();
        }

        private static IHost GetWorkerHost()
        {
            return new HostBuilder()
                .ConfigureServices(
                    (ctx, services) =>
                        {
                            services.AddConductorWorker(
                                configuration: Examples.Api.ApiUtil.GetConfiguration()
                            );
                            services.AddConductorWorkflowTask<GetUserInfo>();
                            services.AddConductorWorkflowTask<SendEmail>();
                            services.AddConductorWorkflowTask<SendSms>();
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
    }
}
