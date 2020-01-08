using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace aiala.Backend.Messaging.Templates
{
    public interface IMailMessageTemplate
    {
        MailMessage Setup(MailMessage mailMessage, CultureInfo culture);
    }
}
