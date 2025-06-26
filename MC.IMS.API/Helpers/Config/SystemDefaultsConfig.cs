using MC.IMS.API.Models.Config;

namespace MC.IMS.API.Helpers.Config;

public class SystemDefaultsConfig
{
    public static SystemDefaults Config { get; set; } = new();

    public static void Initialize(IConfiguration configuration)
    {
        Config = configuration.GetSection("SystemDefaults").Get<SystemDefaults>() ?? Config;
    }
}
