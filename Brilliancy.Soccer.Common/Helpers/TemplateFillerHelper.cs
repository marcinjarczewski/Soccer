using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Brilliancy.Soccer.Common.Helpers
{
    public static class TemplateFillerHelper
    {
        public static string FillTemplate(string content, object model)
        {
            var result = content;
            var modelType = model.GetType();
            var match = Regex.Matches(content, @"@Model\.([a-zA-Z0-9]*)").OrderByDescending(o => o.Length).FirstOrDefault(m => m.Success);
            while (match != null)
            {
                var obj = match.Groups[0].Value;
                var group = match.Groups[1].Value;
                string value = "-";
                try
                {
                    var property = modelType.GetProperty(group);
                    value = property.GetValue(model)?.ToString();
                }
                catch
                {
                }
                result = result.Replace(obj, value);
                match = Regex.Matches(result, @"@Model\.([a-zA-Z0-9]*)").OrderByDescending(o => o.Length).FirstOrDefault(m => m.Success);
            }

            return result;
        }
    }
}
