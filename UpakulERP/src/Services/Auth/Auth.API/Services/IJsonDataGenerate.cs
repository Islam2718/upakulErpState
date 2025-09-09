using System.Text.Json;

namespace Auth.API.Services
{
    public interface IJsonDataGenerate : IAsyncDisposable
    {
         Task WriteJsonAsync();
        Task<string> LoadJwtSettingsFromFileAsync(int moduleId);
    }
}
