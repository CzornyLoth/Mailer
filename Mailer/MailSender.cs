using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MimeKit;

namespace Mailer
{
    class MailSender : IDisposable
    {
        SmtpClient client;

        public MailSender(MailSenderOptions options)
        {

            client = new SmtpClient();
            // client.Connect(options.SmtpServer, options.Port, false);
            // client.Authenticate(options.Username, options.Password);         
        }

        public void SendEmails(ICollection<EmailMessage> messages)
        {
            
            foreach (var msg in messages)
            {
                SendMessage(msg);
             
            }

        }

        public void SendMessage(EmailMessage message /*MailSenderOptions options*/)
        {
            try
            {
                Console.WriteLine(message.Body);
                var mimeMessage = new MimeMessage();
                mimeMessage.From.Add(new MailboxAddress("myemail@gmail.com", message.Subject));
                mimeMessage.To.Add(new MailboxAddress(message.Subject, message.Receiver));
                mimeMessage.Body = new TextPart("Wiadomość: ")
                {
                    Text = message.Body
                };
               // client.Send(mimeMessage);
               // Console.WriteLine("The mails has been sended");
                Console.ReadLine();
                
            }

            catch (Exception ex)
            {
                throw ex;
            }

}

public void Dispose()
{
    client.Disconnect(false);
    client.Dispose();
}
    }
}
