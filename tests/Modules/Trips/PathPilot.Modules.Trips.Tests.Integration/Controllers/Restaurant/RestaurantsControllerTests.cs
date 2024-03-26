using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using PathPilot.Modules.Trips.Application.Restaurants.Commands;
using PathPilot.Modules.Trips.Application.Restaurants.Commands.Shared;
using PathPilot.Modules.Trips.Application.Restaurants.DTO;
using PathPilot.Modules.Trips.Domain.Restaurants.Repositories;
using PathPilot.Modules.Trips.Infrastructure.Restaurants.MongoDb.Repositories;
using PathPilot.Modules.Trips.Tests.Integration.Common;
using PathPilot.Modules.Trips.Tests.Integration.Helpers;
using PathPilot.Shared.Tests;

namespace PathPilot.Modules.Trips.Tests.Integration.Controllers.Restaurant;

[Collection("Serialize2")]
public class RestaurantsControllerTests: 
    IClassFixture<TestApplicationFactory>,
    IClassFixture<TestRestaurantsMongoContext>
{// TODO: add tests with deletion policy
    private const string Path = "trips-module/restaurants";
    private readonly HttpClient _client;
    private readonly IRestaurantRepository _restaurantRepository;

    public RestaurantsControllerTests(TestApplicationFactory factory, TestRestaurantsMongoContext context)
    {
        _client = factory.CreateClient();
        _restaurantRepository = new RestaurantRepository(context.Context);
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

        foreach (var restaurant in restaurants)
        {
            var matchingRestaurant = restaurantsDto.FirstOrDefault(r => r.Id == restaurant.Id);
            matchingRestaurant.ShouldNotBeNull();
            matchingRestaurant.AssertRestaurantDtoMatchesRestaurant(restaurant);
        }
    }

    [Fact]
    public async Task Get_ShouldReturnCorrectRestaurant_WhenValidIdProvided()
    {
        // Arrange
        var restaurant = RestaurantFactory.CreateRestaurant(Guid.NewGuid());
        await _restaurantRepository.AddAsync(restaurant);
        
        // Act
        var response = await _client.GetAsync($"{Path}/{restaurant.Id.Value}");
        
        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.OK);
        var restaurantDto = await response.Content.ReadFromJsonAsync<RestaurantDetailsDto>();
        restaurantDto.ShouldNotBeNull();
        restaurant.AssertRestaurantDtoMatchesRestaurant(restaurantDto);
    }

    [Fact]
    public async Task Get_ShouldReturnNotFound_WhenInvalidIdProvided()
    {
        // Arrange
        var invalidId = Guid.NewGuid();
        
        // Act
        var response = await _client.GetAsync($"{Path}/{invalidId}");
        
        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task AddAsync_ShouldReturnUnauthorized_WithoutAuthorization()
    {
        // Arrange
        var command = new CreateRestaurant("RestaurantName", "RestaurantDescription", "CuisineType");

        // Act
        var response = await _client.PostAsJsonAsync($"{Path}", command);

        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.Unauthorized);
    }
    
    [Fact]
    public async Task AddAsync_ShouldReturnCreatedAtActionResult()
    {
        // Arrange
        var command = new CreateRestaurant("RestaurantName", "RestaurantDescription", "CuisineType");
        var userId = Guid.NewGuid();
        Authenticate(userId);

        // Act
        var response = await _client.PostAsJsonAsync($"{Path}", command);

        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.Created);
        response.Headers.Location.ShouldNotBeNull();
        var idFromResponse = response.Headers.Location.AbsoluteUri[(response.Headers.Location.AbsoluteUri.LastIndexOf('/')+1)..];
        var restaurant = await _restaurantRepository.GetAsync(Guid.Parse(idFromResponse));
        restaurant.ShouldNotBeNull();
        restaurant.Name.Value.ShouldBe(command.Name);
        restaurant.Description.Value.ShouldBe(command.Description);
        restaurant.CuisineType.Value.ShouldBe(command.CuisineType);
        restaurant.Address.ShouldBeNull();
        restaurant.MenuItems.ShouldBeEmpty();
        restaurant.Owner.Value.ShouldBe(userId);
    }
    
    [Fact]
    public async Task CloseAsync_ShouldReturnUnauthorized_WithoutAuthorization()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var restaurant = RestaurantFactory.CreateRestaurant(userId);
        await _restaurantRepository.AddAsync(restaurant);
        Authenticate(userId);

        // Act
        var response = await _client.PutAsync($"{Path}/close/{restaurant.Id.Value}",null);

        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.NoContent);
        var restaurantUpdated = await _restaurantRepository.GetAsync(restaurant.Id.Value);
        restaurantUpdated.ShouldNotBeNull();
        restaurantUpdated.IsOpened.ShouldBeFalse();
    }
    
    [Fact]
    public async Task CloseAsync_ShouldReturnNoContent_WhenRestaurantExists()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var restaurant = RestaurantFactory.CreateRestaurant(userId);
        await _restaurantRepository.AddAsync(restaurant);
        Authenticate(userId);

        // Act
        var response = await _client.PutAsync($"{Path}/close/{restaurant.Id.Value}",null);

        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.NoContent);
        var restaurantUpdated = await _restaurantRepository.GetAsync(restaurant.Id.Value);
        restaurantUpdated.ShouldNotBeNull();
        restaurantUpdated.IsOpened.ShouldBeFalse();
    }
    
    [Fact]
    public async Task OpenAsync_ShouldReturnUnauthorized_WithoutAuthorization()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var restaurant = RestaurantFactory.CreateRestaurant(userId);
        restaurant.Close();
        await _restaurantRepository.AddAsync(restaurant);
        Authenticate(userId);

        // Act
        var response = await _client.PutAsync($"{Path}/open/{restaurant.Id.Value}",null);

        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.NoContent);
        var restaurantUpdated = await _restaurantRepository.GetAsync(restaurant.Id.Value);
        restaurantUpdated.ShouldNotBeNull();
        restaurantUpdated.IsOpened.ShouldBeTrue();
    }
    
    [Fact]
    public async Task OpenAsync_ShouldReturnNoContent_WhenRestaurantExists()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var restaurant = RestaurantFactory.CreateRestaurant(userId);
        restaurant.Close();
        await _restaurantRepository.AddAsync(restaurant);
        Authenticate(userId);

        // Act
        var response = await _client.PutAsync($"{Path}/open/{restaurant.Id.Value}",null);

        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.NoContent);
        var restaurantUpdated = await _restaurantRepository.GetAsync(restaurant.Id.Value);
        restaurantUpdated.ShouldNotBeNull();
        restaurantUpdated.IsOpened.ShouldBeTrue();
    }
    
    [Fact]
    public async Task AddDetailedAsync_ShouldReturnUnauthorized_WithoutAuthorization()
    {
        // Arrange
        var command = new CreateDetailedRestaurant(
            "Test Restaurant",
            "Test Description",
            "Test Cuisine",
            "Test City",
            "Test Street",
            "123",
            "12345",
            "Test Country",
            new List<MenuItemRecord>()
            {
                new ("Item 1", "Description 1", 10.0),
                new ("Item 2", "Description 2", 15.0),
            }
        );

        // Act
        var response = await _client.PostAsJsonAsync($"{Path}/detailed", command);

        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.Unauthorized);
    }
    
    [Fact]
    public async Task AddDetailedAsync_ShouldReturnCreatedAtAction_WhenDetailedRestaurantIsAdded()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var command = new CreateDetailedRestaurant(
            "Test Restaurant",
            "Test Description",
            "Test Cuisine",
            "Test City",
            "Test Street",
            "123",
            "12345",
            "Test Country",
            new List<MenuItemRecord>()
            {
                new ("Item 1", "Description 1", 10.0),
                new ("Item 2", "Description 2", 15.0),
            }
        );
        Authenticate(userId);

        // Act
        var response = await _client.PostAsJsonAsync($"{Path}/detailed", command);

        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.Created);
        response.Headers.Location.ShouldNotBeNull();
        var idFromResponse = response.Headers.Location.AbsoluteUri[(response.Headers.Location.AbsoluteUri.LastIndexOf('/')+1)..];
        var restaurant = await _restaurantRepository.GetAsync(Guid.Parse(idFromResponse));
        restaurant.ShouldNotBeNull();
        restaurant.Name.Value.ShouldBe(command.Name);
        restaurant.Description.Value.ShouldBe(command.Description);
        restaurant.CuisineType.Value.ShouldBe(command.CuisineType);
        restaurant.Address.ShouldNotBeNull();
        restaurant.MenuItems.ShouldNotBeEmpty();
        restaurant.Owner.Value.ShouldBe(userId);
    }
    
    [Fact]
    public async Task UpdateAddressAsync_ShouldReturnUnauthorized_WithoutAuthorization()
    {
        // Arrange
        var restaurant = RestaurantFactory.CreateRestaurant(Guid.NewGuid());
        await _restaurantRepository.AddAsync(restaurant);

        var updateAddressCommand = new UpdateAddress(
            restaurant.Id,
            "New City",
            "New Street",
            "456",
            "54321",
            "New Country"
        );

        // Act
        var response = await _client.PutAsJsonAsync($"{Path}/new-address", updateAddressCommand);

        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.Unauthorized);
    }
    
    [Fact]
    public async Task UpdateAddressAsync_ShouldReturnNoContent_WhenAddressIsUpdated()
    {
        // Arrange
        var userId = Guid.NewGuid();
        Authenticate(userId);
        var restaurant = RestaurantFactory.CreateRestaurant(userId);
        await _restaurantRepository.AddAsync(restaurant);

        var updateAddressCommand = new UpdateAddress(
            restaurant.Id,
            "New City",
            "New Street",
            "456",
            "54321",
            "New Country"
        );

        // Act
        var response = await _client.PutAsJsonAsync($"{Path}/new-address", updateAddressCommand);

        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.NoContent);

        // Check if address was updated
        var updatedRestaurant = await _restaurantRepository.GetAsync(restaurant.Id);
        updatedRestaurant.ShouldNotBeNull();
        updatedRestaurant.Address.City.ShouldBe("New City");
        updatedRestaurant.Address.Street.ShouldBe("New Street");
        updatedRestaurant.Address.BuildingNumber.ShouldBe("456");
        updatedRestaurant.Address.PostCode.ShouldBe("54321");
        updatedRestaurant.Address.Country.ShouldBe("New Country");
    }
    
    [Fact]
    public async Task UpdateMenuAsync_ShouldReturnUnauthorized_WithoutAuthorization()
    {
        // Arrange
        var restaurant = RestaurantFactory.CreateRestaurant(Guid.NewGuid());
        await _restaurantRepository.AddAsync(restaurant);

        var newMenuItems = new List<MenuItemRecord>
        {
            new ("New Item 1", "New Description 1", 15.99),
            new ("New Item 2", "New Description 2", 12.99),
            new ("New Item 3", "New Description 3", 18.49)
        };

        var updateMenuCommand = new UpdateMenu(
            restaurant.Id,
            newMenuItems
        );

        // Act
        var response = await _client.PutAsJsonAsync($"{Path}/new-menu", updateMenuCommand);

        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.Unauthorized);
    }
    
    [Fact]
    public async Task UpdateMenuAsync_ShouldReturnNoContent_WhenMenuIsUpdated()
    {
        // Arrange
        var userId = Guid.NewGuid();
        Authenticate(userId);
        var restaurant = RestaurantFactory.CreateRestaurant(userId);
        await _restaurantRepository.AddAsync(restaurant);

        var newMenuItems = new List<MenuItemRecord>
        {
            new ("New Item 1", "New Description 1", 15.99),
            new ("New Item 2", "New Description 2", 12.99),
            new ("New Item 3", "New Description 3", 18.49)
        };

        var updateMenuCommand = new UpdateMenu(
            restaurant.Id,
            newMenuItems
        );

        // Act
        var response = await _client.PutAsJsonAsync($"{Path}/new-menu", updateMenuCommand);

        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.NoContent);

        var updatedRestaurant = await _restaurantRepository.GetAsync(restaurant.Id);
        updatedRestaurant.ShouldNotBeNull();

        foreach (var newItem in newMenuItems)
        {
            updatedRestaurant.MenuItems.ShouldContain(
                menuItem => menuItem.Name == newItem.Name &&
                            menuItem.Description == newItem.Description &&
                            menuItem.Price == newItem.Price
            );
        }
        
        foreach (var menuItem in restaurant.MenuItems)
        {
            updatedRestaurant.MenuItems.ShouldNotContain(menuItem);
        }
    }
    
    [Fact]
    public async Task UpdateMenuAsync_ShouldReturnNoContent_WhenPartOfMenuIsUpdated()
    {
        // Arrange
        var userId = Guid.NewGuid();
        Authenticate(userId);
        var restaurant = RestaurantFactory.CreateRestaurant(userId);
        await _restaurantRepository.AddAsync(restaurant);

        var newMenuItems = new List<MenuItemRecord>
        {
            new ("Item 1", "New Description 1", 15.99),//this is "update"
            new ("New Item 2", "New Description 2", 12.99),
            new ("New Item 3", "New Description 3", 18.49)
        };

        var updateMenuCommand = new UpdateMenu(
            restaurant.Id,
            newMenuItems
        );

        // Act
        var response = await _client.PutAsJsonAsync($"{Path}/new-menu", updateMenuCommand);

        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.NoContent);

        var updatedRestaurant = await _restaurantRepository.GetAsync(restaurant.Id);
        updatedRestaurant.ShouldNotBeNull();

        foreach (var newItem in newMenuItems)
        {
            updatedRestaurant.MenuItems.ShouldContain(
                menuItem => menuItem.Name == newItem.Name &&
                            menuItem.Description == newItem.Description &&
                            menuItem.Price == newItem.Price
            );
        }
        
        updatedRestaurant.MenuItems.ShouldContain(restaurant.MenuItems.First());
        
        foreach (var menuItem in restaurant.MenuItems.Skip(1))
        {
            updatedRestaurant.MenuItems.ShouldNotContain(menuItem);
        }
    }
    
    private void Authenticate(Guid userId)
    {
        var jwt = AuthHelper.CreateJwt(userId.ToString());
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt);
    }
}