using Microsoft.Extensions.Options;
using aiala.Backend.Messaging.Templates;
using aiala.Backend.Messaging.Templates.ConfirmInvitationTemplate;
using aiala.Backend.Messaging.Templates.ConfirmRegistrationTemplate;
using aiala.Backend.Messaging.Templates.ResetPasswordTemplate;
using System;
using System.Globalization;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using xappido.Directory;
using xappido.Directory.Domain.Messages;
using xappido.Messaging;
using xappido.Operations;
using xappido.Output.Mail;

namespace aiala.Backend.Messaging
{
    public class UserMailMessageHandler : MessageHandler<IDirectoryUserNotificationMessage>
    {
        private readonly IOptions<DirectoryLinksOptions> _linkOptions;
        private readonly ISmtpMailService _smtpMailService;

        public UserMailMessageHandler
        (
            IOptions<DirectoryLinksOptions> linkOptions,
            ISmtpMailService smtpMailService)
        {
            _linkOptions = linkOptions;
            _smtpMailService = smtpMailService;
        }

        protected override async Task<OperationMessageHandlerResult> OnHandle(IDirectoryUserNotificationMessage message)
        {
            var culture = (message.Culture ?? "de-ch").ToLower();
            var messageBuilder = new MailMessageBuilder(
                new MailAddress("info@xappido.com", "Microsoft AIALA"),
                new MailAddress(message.Receiver),
                new CultureInfo(culture));

            switch (message)
            {
                case DirectoryAccountInvitedMessage invitedMessage:
                    {
                        var invitationLink = string.Format(_linkOptions.Value.ConfirmInvitation,
                            culture,
                            invitedMessage.Account.Id,
                            WebUtility.UrlEncode(invitedMessage.Account.Invitation.ConfirmationToken));

                        messageBuilder.UseEncoding(Encoding.UTF8).CreateFromTemplate(new ConfirmInvitationMailTemplate(invitationLink));
                        break;
                    }

                case DirectoryRegistrationConfirmationRequiredMessage registrationMessage:
                    {
                        var registrationLink = string.Format(_linkOptions.Value.ConfirmRegistration,
                            culture,
                            registrationMessage.RegistrationId,
                            WebUtility.UrlEncode(registrationMessage.Token));

                        messageBuilder.UseEncoding(Encoding.UTF8).CreateFromTemplate(new ConfirmRegistrationMailTemplate(registrationLink));
                        break;
                    }

                case DirectoryPasswordResetMessage passwordResetMessage:
                    var resetPasswordLink = string.Format(_linkOptions.Value.ResetPassword,
                        culture,
                        WebUtility.UrlEncode(message.Receiver),
                        WebUtility.UrlEncode(passwordResetMessage.Token));

                    messageBuilder.UseEncoding(Encoding.UTF8).CreateFromTemplate(new ResetPasswordMailTemplate(resetPasswordLink));
                    break;

                default:
                    return OperationMessageHandlerResult.Succeed();
            }

            try
            {
                await _smtpMailService.SendMail(messageBuilder.Build());

                return OperationMessageHandlerResult.Succeed();
            }
            catch (Exception ex)
            {
                return OperationMessageHandlerResult.Fail(ex);
            }
        }
    }
}
