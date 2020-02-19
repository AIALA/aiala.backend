using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using aiala.Backend.Data;
using aiala.Backend.Data.Activities;
using aiala.Backend.Models.Directory;
using aiala.Backend.Resources;
using Newtonsoft.Json;
using xappido.Directory.Settings.Services;
using xappido.Operations;
using xappido.Output.Mail;

namespace aiala.Backend.Operations.Notifications
{
    public class UpdateEmergencyNotificationOperation : InputOperation<EmergencyActivity>
    {
        private readonly AppDbContext _dbContext;
        private readonly ISettingsService _settingsService;
        private readonly ISmtpMailService _mailService;

        public UpdateEmergencyNotificationOperation(AppDbContext dbContext, ISettingsService settingsService, ISmtpMailService mailService)
        {
            _dbContext = dbContext;
            _settingsService = settingsService;
            _mailService = mailService;
        }

        protected override async Task<IOperationResult> Execute(EmergencyActivity input)
        {
            if (!ActivityTypeRanges.Emergency.Contains(input.Type))
            {
                return Invalid("Activity type is not an emergency activity type.");
            }

            string eventName;
            switch (input.Type)
            {
                case ActivityType.EmergencyStart:
                    eventName = MailMessages.EmergencyMailStartEventName;
                    break;
                case ActivityType.EmergencyMood:
                    eventName = MailMessages.EmergencyMailUpdateEventName;
                    break;
                case ActivityType.EmergencyEnd:
                    eventName = MailMessages.EmergencyMailEndEventName;
                    break;
                default:
                    throw new NotImplementedException();
            }

            var previousActivities = await _dbContext.EmergencyActivities
                .Include(a => a.ActiveTask)
                .Where(a => a.EmergencyId == input.EmergencyId)
                .OrderBy(a => a.Timestamp)
                .ToListAsync();

            var settings = await _settingsService.Load<TenantSettings>(input.Tenant.Id);
            var localTimeZone = TimeZoneInfo.FindSystemTimeZoneById(settings.TimeZoneId);

            var startActivity = input.Type == ActivityType.EmergencyStart ? input : previousActivities.Single(a => a.Type == ActivityType.EmergencyStart);
            var newActivityLocalTime = TimeZoneInfo.ConvertTime(input.Timestamp, localTimeZone);
            var startActivityLocalTime = TimeZoneInfo.ConvertTime(startActivity.Timestamp, localTimeZone);

            var baseMailText = MailMessages.EmergencyMailMessage
                .Replace("{time}", $"{newActivityLocalTime:t}")
                .Replace("{date}", $"{newActivityLocalTime:d}")
                .Replace("{eventName}", eventName)
                .Replace("{userName}", input.Tenant.Name);
            var subject = MailMessages.EmergencyMailSubject
                .Replace("{time}", $"{startActivityLocalTime:t}")
                .Replace("{date}", $"{startActivityLocalTime:d}");

            if (startActivity.ActiveTask != null)
            {
                baseMailText = baseMailText.Replace("{taskBlock}", MailMessages.EmergencyTaskBlock.Replace("{taskName}", startActivity.ActiveTask.Name));
            }
            else
            {
                baseMailText = baseMailText.Replace("{taskBlock}", "");
            }

            if (previousActivities.Any())
            {
                var protocol = "<ul style='list-style-type: none'>";
                foreach (var activity in previousActivities)
                {
                    var timestamp = TimeZoneInfo.ConvertTime(activity.Timestamp, localTimeZone);
                    protocol += $"<li>({timestamp:T}) ";
                    switch (activity.Type)
                    {
                        case ActivityType.EmergencyStart:
                            protocol += MailMessages.EmergencyMailStartProtocolEntry;
                            break;
                        case ActivityType.EmergencyMood:
                            protocol += GetMoodProtocolEntry(activity.ActivityData);
                            break;
                        case ActivityType.EmergencyEnd:
                            protocol += MailMessages.EmergencyMailEndProtocolEntry;
                            break;
                    }

                    protocol += "</li>";
                }
                protocol += "</ul>";
                baseMailText = baseMailText.Replace("{protocolBlock}", MailMessages.EmergencyProtocolBlock.Replace("{protocol}", protocol));
            }
            else
            {
                baseMailText = baseMailText.Replace("{protocolBlock}", "");
            }

            if (input.Latitude.HasValue && input.Longitude.HasValue)
            {
                var linkText = MailMessages.EmergencyMailLinkText
                    .Replace("{link}", string.Format("https://www.google.com/maps/search/?api=1&query={0},{1}", input.Latitude, input.Longitude));
                baseMailText = baseMailText.Replace("{linkText}", linkText);
            }
            else
            {
                baseMailText = baseMailText.Replace("{linkText}", "");
            }

            var tenantSettings = await _settingsService.Load<TenantSettings>(input.Tenant.Id);
            foreach (var contactId in new[] { tenantSettings.EmergencyContact1Id, tenantSettings.EmergencyContact2Id }.Where(id => id != null))
            {
                var account = await _dbContext.Accounts
                    .Include(a => a.User)
                    .FirstOrDefaultAsync(a => a.Id == contactId && a.Tenant.Id == input.Tenant.Id);
                if (account == null)
                {
                    continue;
                }

                var mailBody = baseMailText.Replace("{recipientName}", $"{account.User.Firstname} {account.User.Lastname}");

                var mail = new MailMessage(new MailAddress("info@xapppido.com", "Microsoft AIALA"), new MailAddress(account.User.Email))
                {
                    BodyEncoding = Encoding.UTF8,
                    SubjectEncoding = Encoding.UTF8,
                    HeadersEncoding = Encoding.UTF8,
                    IsBodyHtml = true,
                    Subject = subject,
                    Body = mailBody
                };

                try
                {
                    await _mailService.SendMail(mail);
                }
                catch (Exception ಠ_ಠ)
                {
                    return Failed(ಠ_ಠ.Message);
                }
            }

            return Succeeded();
        }

        private string GetMoodProtocolEntry(string activityData)
        {
            var data = JsonConvert.DeserializeObject<Dictionary<string, object>>(activityData);
            // JsonConvert deserializes numbers as long by default, in order to cast, it has to be a int
            var mood = (EmergencyMood) Convert.ChangeType(data[EmergencyActivity.MoodDataKey], typeof(int));

            string moodName;
            switch (mood)
            {
                case EmergencyMood.Better:
                    moodName = MailMessages.EmergencyMoodBetter;
                    break;
                case EmergencyMood.Improving:
                    moodName = MailMessages.EmergencyMoodImproving;
                    break;
                case EmergencyMood.Bad:
                    moodName = MailMessages.EmergencyMoodBad;
                    break;
                default:
                    throw new NotImplementedException();
            }

            return MailMessages.EmergencyMailMoodProtocolEntry.Replace("{moodName}", moodName);
        }
    }
}
