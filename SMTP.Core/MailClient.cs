using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading;

namespace SMTP.Core
{
    public class MailClient
    {
        private readonly SmtpClient _smtp;
        private readonly MailMessage _mail;

        public delegate void SendedEventHandler();
        public event SendedEventHandler Sended;

        public MailClient(ClientOptions options)
        {
            _smtp = new SmtpClient();
            _mail = new MailMessage();

            _smtp.Host = options.Host;
            _smtp.Port = options.Port;
            _smtp.EnableSsl = options.EnableSsl;
            _smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            _smtp.UseDefaultCredentials = false;
            _smtp.Credentials = new NetworkCredential(options.Username, options.Password);

            _mail.SubjectEncoding = Encoding.UTF8;
        }

        public void Send(string from, string to, string subject, string body, bool isBodyHtml = true)
        {
            _mail.From = new MailAddress(from);
            _mail.To.Add(new MailAddress(to));

            _mail.Subject = subject;
            _mail.IsBodyHtml = isBodyHtml;
            _mail.Body = body;

            _smtp.Send(_mail);
        }

        public void Send(MailOptions options)
        {
            _mail.From = new MailAddress(options.From);

            if (options.To != null)
            {
                foreach (string to in options.To)
                {
                    _mail.To.Add(new MailAddress(to));
                }
            }

            if (options.Cc != null)
            {
                foreach (string cc in options.Cc)
                {
                    _mail.CC.Add(new MailAddress(cc));
                }
            }

            _mail.Subject = options.Subject;
            _mail.IsBodyHtml = options.IsBodyHtml;
            _mail.Body = options.Body;

            Thread th = new Thread(() =>
            {
                _smtp.Send(_mail);
                Sended?.Invoke();
            });

            th.Start();
        }
    }
}
