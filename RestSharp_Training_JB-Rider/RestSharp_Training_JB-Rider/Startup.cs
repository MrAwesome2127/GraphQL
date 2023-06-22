using Microsoft.Extensions.DependencyInjection;
using RestSharp_Training_JB_Rider.Base;

namespace RestSharp_Training_JB_Rider;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services
            .AddSingleton<IRestLibrary, RestLibrary>()
            .AddScoped<IRestBuilder, RestBuilder>()
            .AddScoped<IRestFactory, RestFactory>();

    }
}