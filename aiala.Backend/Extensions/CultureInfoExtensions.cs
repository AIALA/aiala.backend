using aiala.Backend.Models.Directory;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace aiala.Backend
{
    public static class CultureInfoExtensions
    {
        private static readonly Dictionary<AppCulture, List<string>> _cultureMapping = new Dictionary<AppCulture, List<string>>
        {
            [AppCulture.English] = new List<string>
            {
                "en-us",
                "en"
            },
            [AppCulture.German] = new List<string>
            {
                "de-ch",
                "de"
            },
            [AppCulture.French] = new List<string>
            {
                "fr-ch",
                "fr"
            }
        };

        public static AppCulture ToAppCulture(this CultureInfo culture)
        {
            return _cultureMapping.FirstOrDefault(kvp => kvp.Value.Contains(culture.Name.ToLower())).Key;
        }

        public static CultureInfo ToCultureInfo(this AppCulture culture)
        {
            if (_cultureMapping.TryGetValue(culture, out var value))
            {
                return new CultureInfo(value.First());
            }

            return new CultureInfo(_cultureMapping[default].First());
        }
    }
}
