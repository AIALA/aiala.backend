using System.Globalization;
using System.Net.Mail;

namespace aiala.Backend.Messaging.Templates.ConfirmRegistrationTemplate
{
    public class ConfirmRegistrationMailTemplate : IMailMessageTemplate
    {
        private readonly string _link;

        public ConfirmRegistrationMailTemplate(string link)
        {
            _link = link;
        }

        public MailMessage Setup(MailMessage mailMessage, CultureInfo culture)
        {
            mailMessage.Subject = ConfirmRegistrationTemplateResource.ResourceManager.GetString("Subject", culture);
            mailMessage.Body = ConfirmRegistrationTemplateResource.ResourceManager.GetString("Body", culture);
            mailMessage.Body = mailMessage.Body.Replace("{{ConfirmationLink}}", _link);

            return mailMessage;
        }
    }
}