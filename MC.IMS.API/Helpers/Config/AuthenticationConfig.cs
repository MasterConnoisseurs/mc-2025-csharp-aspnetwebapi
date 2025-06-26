using MC.IMS.API.Models.Config;

namespace MC.IMS.API.Helpers.Config;

public class AuthenticationConfig
{
    public static Authentication Config { get; set; } = new();

    public static void Initialize(IConfiguration configuration)
    {
        Config = configuration.GetSection("Authentication").Get<Authentication>() ?? Config;
    }
}
