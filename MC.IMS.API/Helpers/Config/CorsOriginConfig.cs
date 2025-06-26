using MC.IMS.API.Models.Config;

namespace MC.IMS.API.Helpers.Config;

public class CorsOriginConfig
{
    public static CorsOrigin Config { get; set; } = new();

    public static void Initialize(IConfiguration configuration)
    {
        Config = configuration.GetSection("CorsOrigins").Get<CorsOrigin>() ?? Config;
    }
}
