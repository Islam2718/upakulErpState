using Auth.API.DTO;
using Auth.API.Repositories.Interfaces;
using System.Text.Json;
using Utility.Security;

namespace Auth.API.Services
{
    public class JsonDataGenerate : IJsonDataGenerate//, IDisposable
    {
        IModuleStrategy _strategy;
        public JsonDataGenerate(IModuleStrategy strategy)
        {
            _strategy = strategy;
        }


        public async Task WriteJsonAsync()
        {
            string filePath = Path.Combine(AppContext.BaseDirectory, "Files", "ProjectXIssuerSigningKey.json");
            // Ensure the directory exists
            string directory = Path.GetDirectoryName(filePath)!;
            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);

            if (File.Exists(filePath))
            {
                var lst = _strategy.GetModuleSecretKey(null);
                if (lst.Any())
                {
                    try
                    {
                        await using FileStream createStream = File.Create(filePath);
                        var options = new JsonSerializerOptions { WriteIndented = true };
                        await JsonSerializer.SerializeAsync(createStream, lst, options);
                    }
                    catch
                    {

                    }
                }
            }
        }

        public async Task<string> LoadJwtSettingsFromFileAsync(int moduleId)
        {
            string filePath = Path.Combine(AppContext.BaseDirectory, "Files", "ProjectXIssuerSigningKey.json");
            if (File.Exists(filePath))
            {
                using FileStream fs = File.OpenRead(filePath);
                var lst = await JsonSerializer.DeserializeAsync<List<ModuleXSecurityKeyVM>>(fs) ?? new List<ModuleXSecurityKeyVM>();
                if (lst.Any(x => x.ModuleId == moduleId))
                    return lst.FirstOrDefault(x => x.ModuleId == moduleId).SecurityKey;
                else return JwtSettings.SecretKey;
            }
            else return JwtSettings.SecretKey;
        }

        public async ValueTask DisposeAsync()
        {
            // If you need to clean up any async resources, do it here
            await Task.CompletedTask;
        }
    }
}
