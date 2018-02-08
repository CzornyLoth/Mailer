using Autofac;
using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Specialized;
using System.Threading.Tasks;

namespace Mailer
{
    class Program
    {
        public static IContainer AutofacContainer = null;

       static void Main(string[] args)
        {
            MailSenderOptions mailerOptions = new MailSenderOptions
            {
                SenderEmail = "myemail@gmail.com",
                SenderName = "myName",
                SmtpServer = "smtp.gmail.com",
                Port = 587,
                Username = "myemail@gmail.com",
                Password = "password",
                
            };

            int refreshInterval = 5;

            ContainerBuilder builder = new ContainerBuilder();
            builder.RegisterInstance(new MailReader(@"C:\test\emails.csv")).As<MailReader>().SingleInstance();
            builder.RegisterInstance(new MailSender(mailerOptions)).As<MailSender>().SingleInstance();
            AutofacContainer = builder.Build();
            RunProgram(refreshInterval).GetAwaiter().GetResult();
            Console.ReadLine();
        }
        private static async Task RunProgram(int refreshInterval)
        {
            try
            {
                // Grab the Scheduler instance from the Factory
                NameValueCollection props = new NameValueCollection
                {
                    { "quartz.serializer.type", "binary" }
                };
                StdSchedulerFactory factory = new StdSchedulerFactory(props);
                IScheduler scheduler = await factory.GetScheduler();

                // and start it off
                await scheduler.Start();

                // define the job and tie it to our HelloJob class
                IJobDetail job = JobBuilder.Create<MailingJob>()
                    .WithIdentity("job", "group")
                    .Build();

                // Trigger the job to run now, and then repeat every 10 seconds
                ITrigger trigger = TriggerBuilder.Create()
                    .WithIdentity("trigger", "group")
                    .StartNow()
                    .WithSimpleSchedule(x => x
                        .WithIntervalInSeconds(refreshInterval)
                        .RepeatForever())
                    .Build();
                await scheduler.ScheduleJob(job, trigger);
            }
            catch (SchedulerException schex)
            {
                Console.WriteLine(schex);
            }
        }
    }
}
