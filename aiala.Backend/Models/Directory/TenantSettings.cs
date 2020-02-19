using System;
using System.ComponentModel.DataAnnotations;
using aiala.Backend.Data.Activities;
using xappido.Directory.Settings.Models;

namespace aiala.Backend.Models.Directory
{
    public class TenantSettings : TenantSettingsBase
    {
        /// <summary>
        /// One of two emergency contacts.
        /// Numbered properties instead of a list because only 2 can be shown in app and for simplicity's sake.
        /// </summary>
        public Guid? EmergencyContact1Id { get; set; }

        /// <summary>
        /// See <see cref="EmergencyContact1Id"/>
        /// </summary>
        public Guid? EmergencyContact2Id { get; set; }

        /// <summary>
        /// First out of two places to be displayed statically in mobile app.
        /// Numbered properties instead of a list because it can't go above 2 places and for simplicity's sake.
        /// </summary>
        public Guid? Place1Id { get; set; }

        /// <summary>
        /// See <see cref="Place1Id"/>.
        /// </summary>
        public Guid? Place2Id { get; set; }

        /// <summary>
        /// Help text to be shown in app when the emergency mood is <see cref="EmergencyMood.Bad"/>.
        /// </summary>
        public string EmergencyTextBad { get; set; }

        /// <summary>
        /// Help text to be shown in app when the emergency mood is <see cref="EmergencyMood.Improving"/>.
        /// </summary>
        public string EmergencyTextImproving { get; set; }

        /// <summary>
        /// See <see cref="TimeZoneInfo.Id"/>.
        /// </summary>
        [Required]
        public string TimeZoneId { get; set; }

        /// <summary>
        /// Tenant culture, in particular concerning server generated messages.
        /// </summary>
        [Required]
        public AppCulture TenantCulture { get; set; }
    }
}
