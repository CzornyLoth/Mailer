using System;
using System.Collections.Generic;
using System.Text;

namespace Mailer
{
    class EmailMessage
    {
        public string Receiver { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}
