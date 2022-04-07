using System;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
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
                         .JobType("determine_quarantine_period")
                         .Handler(DetermineQuarantinePeriod)
                         .MaxJobsActive(5)
                         .Name(Environment.MachineName)
                         .AutoCompletion()
                         .PollInterval(TimeSpan.FromSeconds(1))
                         .Timeout(TimeSpan.FromSeconds(10))
                         .Open();

                zeebeClient.NewWorker()
                         .JobType("notify_person_to_quarantine")
                         .Handler(NotifyPersonToQuarantine)
                         .MaxJobsActive(5)
                         .Name(Environment.MachineName)
                         .AutoCompletion()
                         .PollInterval(TimeSpan.FromSeconds(1))
                         .Timeout(TimeSpan.FromSeconds(10))
                         .Open();

                zeebeClient.NewWorker()
                         .JobType("generate_certificate_of_recovery")
                         .Handler(GenerateCertificateOfRecovery)
                         .MaxJobsActive(5)
                         .Name(Environment.MachineName)
                         .AutoCompletion()
                         .PollInterval(TimeSpan.FromSeconds(1))
                         .Timeout(TimeSpan.FromSeconds(10))
                         .Open();

                zeebeClient.NewWorker()
                         .JobType("send_certificate_of_recovery")
                         .Handler(SendCertificateOfRecovery)
                         .MaxJobsActive(5)
                         .Name(Environment.MachineName)
                         .AutoCompletion()
                         .PollInterval(TimeSpan.FromSeconds(1))
                         .Timeout(TimeSpan.FromSeconds(10))
                         .Open();

                signal.WaitOne();
            }
        }

        private static void DetermineQuarantinePeriod(IJobClient jobClient, IJob job)
        {
            JObject jsonObject = JObject.Parse(job.Variables);
            string person_uuid = (string)jsonObject["person_uuid"];
            bool quarantine_duration_exists = jsonObject.ContainsKey("quarantine_duration");

            Console.WriteLine("Retrieving metadata for person "+ person_uuid +" from external database...");
            Console.WriteLine("Processing Business Rules to determine quarantine duration...");

            if(!quarantine_duration_exists)
            {
                jobClient.NewCompleteJobCommand(job.Key)
                    .Variables("{\"quarantine_duration\": \"P10D\"}")
                    .Send()
                    .GetAwaiter()
                    .GetResult();
                Console.WriteLine("Set quarantine_duration to 10 Days");
            }
        }

        private static void NotifyPersonToQuarantine(IJobClient jobClient, IJob job)
        {
            JObject jsonObject = JObject.Parse(job.Variables);
            string person_uuid = (string)jsonObject["person_uuid"];

            Console.WriteLine("Retrieving contact details for person " + person_uuid + " from external database...");
            Console.WriteLine("Sending notification to person " + person_uuid + " to quarantine...");
        }

        private static void GenerateCertificateOfRecovery(IJobClient jobClient, IJob job)
        {
            Guid uuid = Guid.NewGuid();
            string recovery_certificate_uuid = uuid.ToString();
            JObject jsonObject = JObject.Parse(job.Variables);
            string person_uuid = (string)jsonObject["person_uuid"];

            Console.WriteLine("Generating certificate of recovery for person " + person_uuid + "...");
            Console.WriteLine("Generated certificate ID: " + recovery_certificate_uuid);
            Console.WriteLine("Storing Recovery Certificate in external database...");

            jobClient.NewCompleteJobCommand(job.Key)
                    .Variables("{\"recovery_certificate_uuid\": \"" + recovery_certificate_uuid + "\"}")
                    .Send()
                    .GetAwaiter()
                    .GetResult();
        }

        private static void SendCertificateOfRecovery(IJobClient jobClient, IJob job)
        {
            JObject jsonObject = JObject.Parse(job.Variables);
            string person_uuid = (string)jsonObject["person_uuid"];
            string recovery_certificate_uuid = (string)jsonObject["recovery_certificate_uuid"];

            Console.WriteLine("Retrieving Recovery Certificate " + recovery_certificate_uuid + " from external database...");
            Console.WriteLine("Retrieving contact details for person " + person_uuid + "from external database...");
            Console.WriteLine("Sending Recovery Certificate to person " + person_uuid + ". Enjoy that ice-cream!");
        }
    }
}
