using Autofac;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mailer
{
    class MailingJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            var reader = Program.AutofacContainer.Resolve<MailReader>();
            var items = reader.GetMails().ToList();
            var sender = Program.AutofacContainer.Resolve<MailSender>();
            sender.SendEmails(items);
        }
    }
}
