using GraphQL.Client.Abstractions;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;
using Microsoft.Extensions.DependencyInjection;

namespace GraphQLTest;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton<IGraphQLClient>(new GraphQLHttpClient(new Uri("http://localhost:5000/grapphql"), 
            new NewtonsoftJsonSerializer()));
        
    }
}