using Abdes.Data;
using Newtonsoft.Json;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Abdes.Helpers
{
    public static class Helper
    {
        public static readonly string SettingsFileName = @".\settings.json";

        public static string ComputeSha256Hash(string rawData)
        {
            // Create a SHA256   
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array  
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                // Convert byte array to a string   
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        public static bool IsRegistredScript(string code)
        {
            // Check Registred Script
            var sha256 = Helper.ComputeSha256Hash(code);
            var settings = JsonConvert.DeserializeObject<Settings>(System.IO.File.ReadAllText(SettingsFileName));

            return settings.RegistredScripts.Any(n => n.sha256 == sha256);
        }
    }
}
