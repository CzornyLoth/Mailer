using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using CsvHelper;

namespace Mailer
{
    class MailReader
    {
        public TextReader file;
        public CsvReader reader;
        public MailReader(string filePath)
        {
            file = new StreamReader(filePath);
            reader = new CsvReader(file);
            reader.Configuration.Delimiter = ";";
        }

        public IEnumerable<EmailMessage> GetMails()
        {
            for (int i = 0; i < 100; i++)
            {
                var row = reader.Parser.Read();
                if (row != null && row.Length == 3)
                {
                    yield return new EmailMessage
                    {
                        Receiver = row[0],
                        Subject = row[1],
                        Body = row[2]
                    };

                }
                
            }
        }
    }
}
