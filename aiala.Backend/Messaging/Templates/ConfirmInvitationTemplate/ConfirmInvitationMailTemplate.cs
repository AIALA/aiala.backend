using System.Globalization;
using System.Net.Mail;

namespace aiala.Backend.Messaging.Templates.ConfirmInvitationTemplate
{
    public class ConfirmInvitationMailTemplate : IMailMessageTemplate
    {
        private readonly string _link;

        public ConfirmInvitationMailTemplate(string link)
        {
            _link = link;
        }

        public MailMessage Setup(MailMessage mailMessage, CultureInfo culture)
        {
            mailMessage.Subject = ConfirmInvitationTemplateResource.ResourceManager.GetString("Subject", culture);
            mailMessage.Body = ConfirmInvitationTemplateResource.ResourceManager.GetString("Body", culture);
            mailMessage.Body = mailMessage.Body.Replace("{{ConfirmationLink}}", _link);

            return mailMessage;
        }
    }
}