using System;
using RestSharp;

namespace RestSharpSpecFlow.Drivers
{
    public class Driver
    {
        public Driver(ScenarioContext scenarioContext)
        {
            var restClientOptions = new RestClientOptions
            {
                BaseUrl = new Uri("https://localhost:44330/"),
                RemoteCertificateValidationCallback = (sender, certificate, chain, errors) => true
            };

            //Rest Client
            var restClient = new RestClient(restClientOptions);
            
            //Add into ScenarioContext
            scenarioContext.Add("RestClient", restClient);
        }
    }
}