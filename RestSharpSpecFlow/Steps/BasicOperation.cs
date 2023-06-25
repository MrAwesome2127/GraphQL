using FluentAssertions;
using GraphQLProductApp.Controllers;
using GraphQLProductApp.Data;
using Newtonsoft.Json.Linq;
using RestSharp;
using TechTalk.SpecFlow.Assist;

namespace RestSharpSpecFlow.Steps;

[Binding]
public sealed class BasicOperation
{
    private readonly ScenarioContext _scenarioContext;
    private Product? _response;
    private RestClient _restClient; 

    public BasicOperation(ScenarioContext scenarioContext)
    {
        _scenarioContext = scenarioContext;
        _restClient = _scenarioContext.Get<RestClient>("RestClient");
    }
    
    [Given(@"I perform a GET operation of ""(.*)""")]
    public async Task GivenIPerformAgetOperationOf(string path, Table table)
    {
        dynamic data = table.CreateDynamicInstance();
        var token = PostLoginToken();
        
        var 
            request = new RestRequest(path);
            request.AddUrlSegment("id", (int)data.ProductID);
            request.AddHeader("Authorization", $"Bearer {token}");
        
        _response = await _restClient.GetAsync<Product>(request);
    }

    [Then(@"I validate the Name as ""(.*)""")]
    public void ThenIValidateTheNameAs(string value)
    {
        _response.Name.Should().Be(value);        
    }
    
    private string PostLoginToken()
    {
        var 
            authRequest = new RestRequest("/api/Authenticate/Login");
            authRequest.AddJsonBody(new LoginModel()
            {
                UserName = "TV",
                Password = "TacocaT"
            });
        
        var authResponse = _restClient.PostAsync(authRequest).Result.Content;

        return JObject.Parse(authResponse)["token"].ToString();
    }
}