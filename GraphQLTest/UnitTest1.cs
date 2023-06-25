using GraphQL.Client.Http;
using System;
using FluentAssertions;
using GraphQL;
using GraphQL.Client.Abstractions;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;
using GraphQLProductApp.Data;

namespace GraphQLTest;

public class UnitTest1
{
    private readonly IGraphQLClient _graphQLClient;

    public UnitTest1(IGraphQLClient graphQlClient)
    {
        _graphQLClient = graphQlClient;
    }
    
    [Fact]
    public async Task Test1()
    {
        //Request - Query
        var query = new GraphQLRequest
        {
            Query = @"
            {
                products
                {
                    name
                    price
                    components 
                    {
                        id
                        name   
                    }
                }
            }"
        };

        var response = await _graphQLClient.SendQueryAsync<ProductQueryResponse>(query);
        response.Data.Products.Should().Contain(c => c.Name == "Keyboard");
    }
    
    public class ProductQueryResponse
    {
        public IEnumerable<Product> Products { get; set; }
    }
}