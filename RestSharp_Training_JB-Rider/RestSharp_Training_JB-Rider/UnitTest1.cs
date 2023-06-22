using System.Globalization;
using System.Net;
using FluentAssertions;
using GraphQLProductApp.Controllers;
using GraphQLProductApp.Data;
using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharp_Training_JB_Rider.Base;
using Xunit.Abstractions;

namespace RestSharp_Training_JB_Rider;

public class UnitTest1
{
    private readonly IRestFactory _restFactory;
    private string? _token;
    public UnitTest1(IRestFactory restFactory)
    {
        _restFactory = restFactory;
        _token = GetToken();
    }

    [Fact]
    public async Task GetProductId()
    {
        var response = await _restFactory
            .Create()
                .WithRequest("/Product/GetProductById/1")
                .WithHeader("Authorization", $"Bearer {_token}")
                .WithGet<Product>();
        response?.Name.Should().Be("Keyboard");
    }
    
    [Fact]
    public async Task GetQuerySegment()
    {
        var response = await _restFactory
            .Create()
                .WithRequest("/Product/GetProductById/{id}")
                .WithHeader("Authorization", $"Bearer {_token}")
                .WithUrlSegment("id", "2")
                .WithGet<Product>();
        response?.Price.Should().Be(400);
    }
    
    [Fact]
    public async Task GetQueryParameters()
    {
        var response = await _restFactory
            .Create()
                .WithRequest("/Product/GetProductByIdAndName")
                .WithQueryParameter("id", "2")
                .WithQueryParameter("name", "Monitor")
                .WithHeader("Authorization", $"Bearer {_token}")
                .WithGet<Product>();
        response?.Price.Should().Be(400);
    }
    
    [Fact]
    public async Task PostProductTest()
    {
        var response = await _restFactory
            .Create()
                .WithRequest("/Product/Create")
                .WithBody(new Product
                {
                    Name = "Cabinet",
                    Description = "Gaming Cabinet",
                    Price = 300,
                    ProductType = ProductType.PERIPHARALS
                })
                .WithHeader("Authorization", $"Bearer {_token}")
                .WithPost<Product>();
        response?.Price.Should().Be(300);
    }
    
    // [Fact]
    // public async Task PostFileUpload()
    // {
    //     //Rest Request
    //     var request = new RestRequest("/Product",Method.Post);
    //     request.AddHeader("Authorization", $"Bearer {GetToken( )}");
    //     request.AddFile("myFile", @"C:\Users\Coach\OneDrive\Pictures\Bug.jpg","multipart/form-data");
    //
    //     //Perform Operations <Post>
    //     var response = await _client.ExecuteAsync(request);
    //
    //     //Assert
    //     response.StatusCode.Should().Be(HttpStatusCode.Created);
    // }

    private string GetToken()
    {
        var authResponse = _restFactory
            .Create()
                .WithRequest("/api/Authenticate/Login")
                .WithBody(new LoginModel
                {
                    UserName = "TV",
                    Password = "TacocaT"
                })
                .WithPost().Result.Content;
        //Returns the Token
        return JObject.Parse(authResponse)["token"].ToString();
    }
}