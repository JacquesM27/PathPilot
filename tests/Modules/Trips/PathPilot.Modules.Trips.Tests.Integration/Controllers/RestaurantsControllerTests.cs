using System.Net;
using PathPilot.Modules.Trips.Infrastructure.Restaurants.MongoDb;
using PathPilot.Modules.Trips.Tests.Integration.Common;
using PathPilot.Shared.Tests;
using Shouldly;

namespace PathPilot.Modules.Trips.Tests.Integration.Controllers;

[Collection("integration")]
public class RestaurantsControllerTests : 
    IClassFixture<TestApplicationFactory>,
    IClassFixture<TestRestaurantsMongoContext>
{
    private const string Path = "trips-module/restaurants";
    private HttpClient _client;
    private RestaurantsMongoContext _context;

    public RestaurantsControllerTests(TestApplicationFactory factory, TestRestaurantsMongoContext context)
    {
        _client = factory.CreateClient();
        _context = context.Context;
    }

    [Fact]
    public async Task GetBrowse_ShouldReturnEmptyCollection()
    {
        // Arrange
        // Act
        var response = await _client.GetAsync($"{Path}");
        
        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.OK);
    }
}