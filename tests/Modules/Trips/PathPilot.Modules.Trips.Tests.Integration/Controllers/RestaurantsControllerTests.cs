using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using PathPilot.Modules.Trips.Application.Restaurants.DTO;
using PathPilot.Modules.Trips.Domain.Restaurants.Repositories;
using PathPilot.Modules.Trips.Infrastructure.Restaurants.MongoDb;
using PathPilot.Modules.Trips.Infrastructure.Restaurants.MongoDb.Repositories;
using PathPilot.Modules.Trips.Tests.Integration.Common;
using PathPilot.Modules.Trips.Tests.Integration.Helpers;
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
    private IRestaurantRepository _restaurantRepository;

    public RestaurantsControllerTests(TestApplicationFactory factory, TestRestaurantsMongoContext context)
    {
        _client = factory.CreateClient();
        _context = context.Context;
        _restaurantRepository = new RestaurantRepository(_context);
    }

    [Fact]
    public async Task GetBrowse_ShouldReturnEmptyCollection_WhenNoRestaurantsExist()
    {
        // Arrange
        // Act
        var response = await _client.GetAsync($"{Path}");
    
        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.OK);

        var content = await response.Content.ReadAsStringAsync();
        var restaurants = JsonSerializer.Deserialize<List<RestaurantDto>>(content);
        restaurants.ShouldNotBeNull();
        restaurants.ShouldBeEmpty();
    }

    [Fact]
    public async Task GetBrowse_ShouldReturnNotEmptyCollection_WhenRestaurantsExist()
    {
        // Arrange
        var restaurants = RestaurantFactory.CreateTwoRestaurants();
        foreach (var restaurant in restaurants)
        {
            await _restaurantRepository.AddAsync(restaurant);
        }
        
        // Act
        var response = await _client.GetAsync($"{Path}");
    
        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.OK);

        var restaurantsDto = await response.Content.ReadFromJsonAsync<List<RestaurantDto>>();
        restaurantsDto.ShouldNotBeNull();
        restaurantsDto.ShouldNotBeEmpty();
        
        foreach (var restaurantDto in restaurantsDto)
        {
            var matchingRestaurant = restaurants.FirstOrDefault(r => r.Id == restaurantDto.Id);
            matchingRestaurant.ShouldNotBeNull();

            restaurantDto.Id.ShouldBe(matchingRestaurant.Id.Value);
            restaurantDto.Name.ShouldBe(matchingRestaurant.Name);
            restaurantDto.Description.ShouldBe(matchingRestaurant.Description);
            restaurantDto.IsOpened.ShouldBe(matchingRestaurant.IsOpened);
            restaurantDto.AverageRate.ShouldBe(matchingRestaurant.AverageRate);
            restaurantDto.CuisineType.ShouldBe(matchingRestaurant.CuisineType);
        }
    }
}