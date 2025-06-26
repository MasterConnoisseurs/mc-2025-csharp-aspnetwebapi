using MC.IMS.API.Models.Config;

namespace MC.IMS.API.Helpers.Config;

public class ConnectionStringsConfig
{
    public static ConnectionStrings Config { get; set; } = new();

    public static void Initialize(IConfiguration configuration)
    {
        Config = configuration.GetSection("ConnectionStrings").Get<ConnectionStrings>() ?? Config;
    }
}
