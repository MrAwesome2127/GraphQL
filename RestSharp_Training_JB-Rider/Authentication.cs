using FluentAssertions;
using GraphQLProductApp.Controllers;
using GraphQLProductApp.Data;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace RestSharp_Training_JB_Rider;

public class Authentication
{
    private RestClientOptions restClientOptions;
    public Authentication()
    {
        restClientOptions = new RestClientOptions
        {
            BaseUrl = new Uri("https://localhost:44330"),
            RemoteCertificateValidationCallback = (sender, certificate, chain, errors) => true
        };
    }

    [Fact]
    public async Task PostLoginToken()
    {
        //Rest Client
        var client = new RestClient(restClientOptions);

        //Rest Request
        var authRequest = new RestRequest("/api/Authenticate/Login");
        
        // //Anonymous object being passed as body in request
        // request.AddJsonBody(new
        // {
        //     username = "TV",
        //     password = "TacocaT"
        // });
        
        //Typed object being passed as body in request **Preferred**
        authRequest.AddJsonBody(new LoginModel()
        {
            UserName = "TV",
            Password = "TacocaT"
        });
    
        //Perform Operations <GET>
        var authResponse = client.PostAsync(authRequest).Result.Content;
        
        //Capture Token
        var token = JObject.Parse(authResponse)["token"];
        
        //Rest Client
        var AuthResponse = new RestClient(restClientOptions);
        
        //Rest Request
        var GetPoductRequest = new RestRequest(resource:"/Product/GetProductById/1");
        GetPoductRequest.AddHeader("Authorization", $"Bearer {token?.ToString()}");

        //Perform Operations <GET>
        var GetPoductResponse = await client.GetAsync<Product>(GetPoductRequest);

        //Assert
        GetPoductResponse?.Name.Should().Be("Keyboard");
        token.Should().NotBeNull();
    }
}