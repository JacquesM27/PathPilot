using NSubstitute;
using PathPilot.Modules.Trips.Application.Restaurants.DTO;
using PathPilot.Modules.Trips.Application.Restaurants.Queries;
using PathPilot.Modules.Trips.Application.Restaurants.Queries.Handlers;
using PathPilot.Modules.Trips.Domain.Restaurants.Entities;
using PathPilot.Modules.Trips.Domain.Restaurants.Repositories;
using PathPilot.Modules.Trips.Domain.Tests.Helpers;
using PathPilot.Shared.Abstractions.Kernel.Types;
using PathPilot.Shared.Abstractions.Queries;
using Shouldly;

namespace PathPilot.Modules.Trips.Domain.Tests.Queries.Handlers
{
    public class BrowseRestaurantsHandlerTests
    {
        private readonly IRestaurantRepository _restaurantRepository;
        private readonly IQueryHandler<BrowseRestaurants, IEnumerable<RestaurantDto>> _queryHandler;
        private readonly IEnumerable<Restaurant> _restaurants;

        public BrowseRestaurantsHandlerTests()
        {
            _restaurantRepository = Substitute.For<IRestaurantRepository>();
            _queryHandler = new BrowseRestaurantsHandler(_restaurantRepository);
            _restaurants = RestaurantHelper.GetRestaurants();
        }

        private Task<IEnumerable<RestaurantDto>> Act(BrowseRestaurants query) => _queryHandler.HandleAsync(query);

        [Fact]
        public async Task HandleBrowseRestaurants_ShouldReturnAllRestaurants_WhenIdsNotProvided()
        {
            // Arrange
            var query = new BrowseRestaurants();
            _restaurantRepository.BrowseAsync().Returns(_restaurants);

            // Act
            var restaurantDtos = (await Act(query)).ToList();

            // Assert
            restaurantDtos.ShouldNotBeNull();
            restaurantDtos.ShouldBeOfType<List<RestaurantDto>>();
            restaurantDtos.Count.ShouldBe(_restaurants.Count());
        }

        [Fact]
        public async Task HandleBrowseRestaurants_ShouldReturnFilteredRestaurants_WhenIdsProvided()
        {
            // Arrange
            var ids = new[] { "1", "2", "3" };
            var query = new BrowseRestaurants(ids);
            var filteredRestaurants = _restaurants.Where(r => ids.Contains(r.Id.Value)).ToArray();
            _restaurantRepository.BrowseAsync(Arg.Any<IEnumerable<EntityId>>()).Returns(filteredRestaurants);

            // Act
            var restaurantDtos = (await Act(query)).ToArray();

            // Assert
            restaurantDtos.ShouldNotBeNull();
            restaurantDtos.ShouldBeOfType<RestaurantDto[]>();
            restaurantDtos.Length.ShouldBe(filteredRestaurants.Count());
        }
    }
}
