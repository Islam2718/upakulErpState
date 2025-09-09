using Newtonsoft.Json;
using Ocelot.Configuration.File;

namespace Gateway.Api.Extensions
{
    public static class ConfigurationBuilderExtensions
    {
        public static IConfigurationBuilder AddOcelotConfigFiles(this IConfigurationBuilder builder, string folder, string[] appNames, IWebHostEnvironment env)
        {
            const string primaryConfigFile = "service.json";
            const string globalConfigFile = "service.global.json";
                var files = new DirectoryInfo(folder)
                .EnumerateFiles()
                .Where(fi => fi.Name.Contains($"service.{env.EnvironmentName}.json") && appNames.Any(e => fi.Name.Contains(e))).ToList();
            var fileConfiguration = new FileConfiguration();
            foreach (var file in files)
            {
                if(files.Count>1&& file.Name.Equals(primaryConfigFile,StringComparison.OrdinalIgnoreCase) ) continue;
                var lines=File.ReadAllText(file.FullName);
                var config=JsonConvert.DeserializeObject<FileConfiguration>(lines);
                if (file.Name.Equals(globalConfigFile, StringComparison.OrdinalIgnoreCase))
                    fileConfiguration.GlobalConfiguration = config.GlobalConfiguration;
                fileConfiguration.Aggregates.AddRange(config.Aggregates);
                fileConfiguration.Routes.AddRange(config.Routes);
            }
            var json=JsonConvert.SerializeObject(fileConfiguration);
            File.WriteAllText(primaryConfigFile, json);
            builder.AddJsonFile(primaryConfigFile,optional:false,reloadOnChange:true);
            return builder;
        }
    }
}
