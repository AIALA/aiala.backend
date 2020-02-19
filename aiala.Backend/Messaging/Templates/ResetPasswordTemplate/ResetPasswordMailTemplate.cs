using System.Globalization;
using System.Net.Mail;

namespace aiala.Backend.Messaging.Templates.ResetPasswordTemplate
{
    public class ResetPasswordMailTemplate : IMailMessageTemplate
    {
        private readonly string _link;

        public ResetPasswordMailTemplate(string link)
        {
            _link = link;
        }

        public MailMessage Setup(MailMessage mailMessage, CultureInfo culture)
        {
            mailMessage.Subject = ResetPasswordTemplateResource.ResourceManager.GetString("Subject", culture);
            mailMessage.Body = ResetPasswordTemplateResource.ResourceManager.GetString("Body", culture);
            mailMessage.Body = mailMessage.Body.Replace("{{ResetPasswordLink}}", _link);

            return mailMessage;
        }
    }
}