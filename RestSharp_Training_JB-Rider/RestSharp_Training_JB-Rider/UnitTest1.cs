using System.Globalization;
using System.Net;
using FluentAssertions;
using GraphQLProductApp.Controllers;
using GraphQLProductApp.Data;
using Newtonsoft.Json.Linq;
using RestSharp;
using Xunit.Abstractions;

namespace RestSharp_Training_JB_Rider;

public class UnitTest1
{
    private RestClientOptions restClientOptions;
    public UnitTest1()
    {
        restClientOptions = new RestClientOptions
        {
            BaseUrl = new Uri("https://localhost:44330"),
            RemoteCertificateValidationCallback = (sender, certificate, chain, errors) => true
        };
    }
    
    [Fact]
    public async Task Test1()
    {
        //Rest Client
        var client = new RestClient(restClientOptions);
        
        //Rest Request
        var request = new RestRequest(resource:"/Product/GetProductById/1");
        request.AddHeader("Authorization", $"Bearer {GetToken( )}");
        
        //Perform Operations <GET>
        var response = await client.GetAsync<Product>(request);
        
        //Assert
        response?.Name.Should().Be("Keyboard");
    }
    
    [Fact]
    public async Task GetQuerySegment()
    {
        //Rest Client
        var client = new RestClient(restClientOptions);
        
        //Rest Request
        var request = new RestRequest("/Product/GetProductById/{id}");
        request.AddHeader("Authorization", $"Bearer {GetToken( )}");
        request.AddUrlSegment("id", 2);

        //Perform Operations <GET>
        var response = await client.GetAsync<Product>(request);
        
        //Assert
        response?.Price.Should().Be(400);
    }
    
    [Fact]
    public async Task GetQueryParameters()
    {
        //Rest Client
        var client = new RestClient(restClientOptions);

        //Rest Request
        var request = new RestRequest("/Product/GetProductByIdAndName");
        request.AddHeader("Authorization", $"Bearer {GetToken( )}");
        request.AddQueryParameter("id", 2);
        request.AddQueryParameter("name", "Monitor");

        //Perform Operations <GET>
        var response = await client.GetAsync<Product>(request);

        //Assert
        response?.Price.Should().Be(400);
    }
    
    [Fact]
    public async Task PostProductTest()
    {
        //Rest Client
        var client = new RestClient(restClientOptions);

        //Rest Request
        var request = new RestRequest("/Product/Create");
        request.AddHeader("Authorization", $"Bearer {GetToken( )}");
        request.AddJsonBody(new Product()
        {
            Name = "Cabinet",
            Description = "Gaming Cabinet",
            Price = 300,
            ProductType = ProductType.PERIPHARALS
        });

        //Perform Operations <POST>
        var response = await client.PostAsync<Product>(request);

        //Assert
        response?.Price.Should().Be(300);
    }
    
    [Fact]
    public async Task PostFileUpload()
    {
        //Rest Client
        var client = new RestClient(restClientOptions);

        //Rest Request
        var request = new RestRequest("/Product",Method.Post);
        request.AddHeader("Authorization", $"Bearer {GetToken( )}");
        request.AddFile("myFile", @"C:\Users\Coach\OneDrive\Pictures\Bug.jpg","multipart/form-data");

        //Perform Operations <Post>
        var response = await client.ExecuteAsync(request);

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.Created);
    }

    private string GetToken()
    {
        //Rest Client
        var client = new RestClient(restClientOptions);

        //Rest Request
        var authRequest = new RestRequest("/api/Authenticate/Login");
        authRequest.AddJsonBody(new LoginModel()
        {
            UserName = "TV",
            Password = "TacocaT"
        });
    
        //Perform Operations <GET>
        var authResponse = client.PostAsync(authRequest).Result.Content;
        
        //Returns the Token
        return JObject.Parse(authResponse)["token"].ToString();
    }
}