using System.Runtime.Serialization;

namespace aiala.Backend.Models.Directory
{
    public enum AppCulture
    {
        [EnumMember(Value = "en-us")]
        English = 0,

        [EnumMember(Value = "de-ch")]
        German = 1,

        [EnumMember(Value = "fr-ch")]
        French = 2,

        [EnumMember(Value = "es-es")]
        Spanish = 3,

        [EnumMember(Value = "it-ch")]
        Italian = 3
    }
}
