using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using qwitix_api.Core.Mappers.EventMappers;
using qwitix_api.Core.Services.EventService.DTOs;

namespace qwitix_api_unit_tests.MapperTests
{
    [TestClass]
    public class CreateEventMapperTests
    {
        [TestMethod]
        public void ToEntity_ValidDTO_ReturnsCorrectEvent()
        {
            // Arrange
            var dto = new CreateEventDTO
            {
                OrganizerId = "organizer123",
                Title = "Rock Concert",
                Description = "An amazing rock concert.",
                Category = "Music",
                Venue = new CreateVenueDTO
                {
                    Name = "Stadium",
                    Address = "123 Street",
                    City = "Metropolis",
                    State = "NY",
                    Zip = "10001",
                },
            };

            var mapper = new CreateEventMapper();

            // Act
            var result = mapper.ToEntity(dto);

            // Assert
            Assert.AreEqual(dto.OrganizerId, result.OrganizerId);
            Assert.AreEqual(dto.Title, result.Title);
            Assert.AreEqual(dto.Description, result.Description);
            Assert.AreEqual(dto.Category, result.Category);

            Assert.IsNotNull(result.Venue);
            Assert.AreEqual(dto.Venue.Name, result.Venue.Name);
            Assert.AreEqual(dto.Venue.Address, result.Venue.Address);
            Assert.AreEqual(dto.Venue.City, result.Venue.City);
            Assert.AreEqual(dto.Venue.State, result.Venue.State);
            Assert.AreEqual(dto.Venue.Zip, result.Venue.Zip);
        }
    }
}
