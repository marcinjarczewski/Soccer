using Brilliancy.Soccer.Common.Enums;
using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Brilliancy.Soccer.Common.Helpers
{

    public static class LanguageHelper
    {
        public static LanguageEnum GetLanguage(string languageCode)
        {
            var code = languageCode?.ToLower() ?? "";
            switch (code)
            {
                case "en":
                    return LanguageEnum.English;
                default:
                    return LanguageEnum.Polish;
            }
        }
    }
}
