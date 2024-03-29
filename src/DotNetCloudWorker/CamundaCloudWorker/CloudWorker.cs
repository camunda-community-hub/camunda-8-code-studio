﻿using System;
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
                                .UseClientId("jHVr.3DQ~3ApfHfFJ4msPgGl~a6Pjv.G")
                                .UseClientSecret("iJAUCN5j.y_zt0khCgo2gvfU08FxZuMM0n.ld9yeEyqvP~L7hxRQiQdK.6FqTyAW")
                                .UseContactPoint("dbd4cad1-5621-4d66-b14e-71c92456939a.bru-2.zeebe.camunda.io")
                                .Build();

            using (var signal = new EventWaitHandle(false, EventResetMode.AutoReset))
            {
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
                         .Handler(NotifyPersonToQuarantine)
                         .MaxJobsActive(5)
                         .Name(Environment.MachineName)
                         .AutoCompletion()
                         .PollInterval(TimeSpan.FromSeconds(1))
                         .Timeout(TimeSpan.FromSeconds(10))
                         .Open();

                signal.WaitOne();
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
