using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace aiala.Backend.Messaging.Templates
{
    public class MailMessageBuilder
    {
        private readonly MailMessage _mailMessage;
        private readonly CultureInfo _culture;

        public MailMessageBuilder(MailAddress from, MailAddress to, CultureInfo culture)
        {
            _culture = culture;
            _mailMessage = new MailMessage(from, to);
        }

        public MailMessage Build() => _mailMessage;

        public MailMessageBuilder UseEncoding(Encoding encoding)
        {
            _mailMessage.BodyEncoding = encoding;
            _mailMessage.SubjectEncoding = encoding;
            _mailMessage.HeadersEncoding = encoding;
            return this;
        }

        public MailMessageBuilder CreateFromTemplate<TTemplate>(TTemplate template) where TTemplate : IMailMessageTemplate
        {
            _mailMessage.IsBodyHtml = true;
            template.Setup(_mailMessage, _culture);

            return this;
        }
    }
}
