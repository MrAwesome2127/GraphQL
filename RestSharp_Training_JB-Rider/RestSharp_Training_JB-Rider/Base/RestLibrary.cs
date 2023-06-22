using RestSharp;

namespace RestSharp_Training_JB_Rider.Base;

public interface IRestLibrary
{
    RestClient RestClient { get; }
}

public class RestLibrary : IRestLibrary
{
    public RestLibrary()
    {
        var restClientOptions = new RestClientOptions
        {
            BaseUrl = new Uri("https://localhost:44330"),
            RemoteCertificateValidationCallback = (sender, certificate, chain, errors) => true
        };
        
        //Rest Client
        RestClient = new RestClient(restClientOptions);
    }
     
    public RestClient RestClient { get; }
}