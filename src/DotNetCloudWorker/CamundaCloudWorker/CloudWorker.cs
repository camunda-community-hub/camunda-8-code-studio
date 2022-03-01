using System;
using System.Threading;
using System.Threading.Tasks;
using Zeebe.Client.Api.Responses;
using Zeebe.Client.Api.Worker;
using Zeebe.Client.Impl.Builder;

namespace CamundaCloudWorker
{
    class Program
    {
        static async Task Main(string[] args)
        {

            var zeebeClient = CamundaCloudClientBuilder
                                .Builder()
                                .UseClientId("")
                                .UseClientSecret("")
                                .UseContactPoint("")
                                .Build();

            using (var signal = new EventWaitHandle(false, EventResetMode.AutoReset))
            {
                zeebeClient.NewWorker()
                      .JobType("worker")
                      .Handler(HandleJob)
                      .MaxJobsActive(5)
                      .Name(Environment.MachineName)
                      .AutoCompletion()
                      .PollInterval(TimeSpan.FromSeconds(1))
                      .Timeout(TimeSpan.FromSeconds(10))
                      .Open();

                // blocks main thread, so that worker can run
                signal.WaitOne();
            }
        }

        private static void HandleJob(IJobClient jobClient, IJob job)
        {
            // business logic
        }
    }
}
