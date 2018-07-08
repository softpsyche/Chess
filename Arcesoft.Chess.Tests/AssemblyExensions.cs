using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Arcesoft.Chess.Tests
{
    public static class AssemblyExensions
    {
        public static string EmbeddedResourceString(this Assembly assembly, string name)
            => Encoding.UTF8.GetString(assembly.EmbeddedResource(name));

        public static byte[] EmbeddedResource(this Assembly assembly, string name)
        {
            var names = assembly.GetManifestResourceNames().Where(a => a.EndsWith(name)).ToList();

            if (names.Count == 0)
            {
                throw new Exception($"No embedded resource with name ending in '{name}' found.");
            }
            else if (names.Count > 1)
            {
                throw new Exception($"More than one embedded resource with name ending in '{name}' found.");
            }

            using (var stream = assembly.GetManifestResourceStream(names.Single()))
            {
                var buffer = new byte[stream.Length];
                stream.Read(buffer, 0, Convert.ToInt32(stream.Length));
                return buffer;
            }
        }
    }
}
