using System.Collections.Generic;

namespace SMTP.Core
{
    public class MailOptions
    {
        public string From { get; set; }
        public ICollection<string> To { get; set; }
        public ICollection<string> Cc { get; set; }

        public string Subject { get; set; }
        public bool IsBodyHtml { get; set; }
        public string Body { get; set; }

        public MailOptions()
        {
            To = new List<string>();
            Cc = new List<string>();

            IsBodyHtml = true;
        }
    }
}
