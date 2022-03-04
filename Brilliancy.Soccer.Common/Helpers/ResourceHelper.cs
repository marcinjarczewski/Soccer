using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Brilliancy.Soccer.Common.Helpers
{

    public static class ResourceHelper
    {
        private static Stream GetResourceAsStream(string resourceName, Type resourceAssembly)
        {
            var assembly = Assembly.GetAssembly(resourceAssembly);
            return assembly.GetManifestResourceStream(assembly.GetManifestResourceNames().FirstOrDefault(x => x.IndexOf(resourceName) != -1));
        }

        public static string GetResourceAsString(string resourceName, Type resourceAssembly)
        {
            using (var inputStream = GetResourceAsStream(resourceName, resourceAssembly))
            {
                using (var reader = new StreamReader(inputStream))
                {
                    return reader.ReadToEnd();
                }
            }
        }
    }
}
