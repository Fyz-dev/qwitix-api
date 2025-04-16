using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using qwitix_api.Core.Enums;
using qwitix_api.Core.Mappers.EventMappers;
using qwitix_api.Core.Models;
using qwitix_api.Core.Services.EventService.DTOs;

namespace qwitix_api_unit_tests.MapperTests
{
    [TestClass]
    public class ResponseEventMapperTests
    {
        [TestMethod]
        public void ToDto_ValidEvent_ReturnsCorrectDTO()
        {
            // Arrange
            var eventModel = new Event
            {
                Id = "event123",
                OrganizerId = "organizer456",
                Title = "Jazz Night",
                Description = "A smooth jazz evening.",
                Category = "Music",
                Status = EventStatus.Draft,
                Venue = new Venue
                {
                    Name = "City Hall",
                    Address = "456 Avenue",
                    City = "New York",
                    State = "NY",
                    Zip = "10002",
                },
                StartDate = new DateTime(2025, 6, 10),
                EndDate = new DateTime(2025, 6, 11),
                CreatedAt = new DateTime(2025, 4, 1),
                UpdatedAt = new DateTime(2025, 4, 5),
            };

            var mapper = new ResponseEventMapper();

            // Act
            var dto = mapper.ToDto(eventModel);

            // Assert
            Assert.AreEqual(eventModel.Id, dto.Id);
            Assert.AreEqual(eventModel.OrganizerId, dto.OrganizerId);
            Assert.AreEqual(eventModel.Title, dto.Title);
            Assert.AreEqual(eventModel.Description, dto.Description);
            Assert.AreEqual(eventModel.Category, dto.Category);
            Assert.AreEqual(eventModel.Status, dto.Status);
            Assert.AreEqual(eventModel.StartDate, dto.StartDate);
            Assert.AreEqual(eventModel.EndDate, dto.EndDate);
            Assert.AreEqual(eventModel.CreatedAt, dto.CreatedAt);
            Assert.AreEqual(eventModel.UpdatedAt, dto.UpdatedAt);

            Assert.IsNotNull(dto.Venue);
            Assert.AreEqual(eventModel.Venue.Name, dto.Venue.Name);
            Assert.AreEqual(eventModel.Venue.Address, dto.Venue.Address);
            Assert.AreEqual(eventModel.Venue.City, dto.Venue.City);
            Assert.AreEqual(eventModel.Venue.State, dto.Venue.State);
            Assert.AreEqual(eventModel.Venue.Zip, dto.Venue.Zip);
        }
    }
}
