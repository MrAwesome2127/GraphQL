using Microsoft.AspNetCore.Mvc.Testing;
using RestSharp;

namespace RestSharp_Training_JB_Rider.Base;

public interface IRestLibrary
{
    RestClient RestClient { get; }
}

public class RestLibrary : IRestLibrary
{
    public RestLibrary(WebApplicationFactory<GraphQLProductApp.Startup> webApplicationFactory)
    {
        var restClientOptions = new RestClientOptions
        {
            BaseUrl = new Uri("https://localhost:5001"),
            RemoteCertificateValidationCallback = (sender, certificate, chain, errors) => true
        };

        //Spawn system under test
        var client = webApplicationFactory.CreateDefaultClient();
        
        //Rest Client
        RestClient = new RestClient(client, restClientOptions);
    }
     
    public RestClient RestClient { get; }
}