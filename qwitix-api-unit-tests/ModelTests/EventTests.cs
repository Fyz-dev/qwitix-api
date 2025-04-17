using System.ComponentModel.DataAnnotations;
using qwitix_api.Core.Enums;
using qwitix_api.Core.Models;

namespace qwitix_api_unit_tests
{
    [TestClass]
    public class EventTests
    {
        private Venue GetValidVenue() =>
            new Venue
            {
                Name = "Test Venue",
                Address = "Test Address",
                City = "Test City",
            };

        [TestMethod]
        public void CreateEvent_WithValidData_ShouldSucceed()
        {
            var ev = new Event
            {
                OrganizerId = "660f7b627e5e9a9f48a0e4d3",
                Title = "Valid Title",
                Description = "Short Description",
                Category = "Concert",
                Venue = GetValidVenue(),
                StartDate = DateTime.UtcNow.AddDays(1),
                EndDate = DateTime.UtcNow.AddDays(2),
                Status = EventStatus.Scheduled,
            };

            Assert.AreEqual("Valid Title", ev.Title);
            Assert.AreEqual(EventStatus.Scheduled, ev.Status);
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void SetOrganizerId_ToNull_ShouldThrow()
        {
            var ev = new Event { OrganizerId = null! };
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void SetTitle_ToEmpty_ShouldThrow()
        {
            var ev = new Event { Title = "" };
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void SetTitle_ToTooLong_ShouldThrow()
        {
            var ev = new Event { Title = new string('A', 251) };
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void SetDescription_TooLong_ShouldThrow()
        {
            var ev = new Event { Description = new string('B', 10001) };
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void SetCategory_ToEmpty_ShouldThrow()
        {
            var ev = new Event { Category = "   " };
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void SetCategory_TooLong_ShouldThrow()
        {
            var ev = new Event { Category = new string('C', 101) };
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void SetVenue_ToNull_ShouldThrow()
        {
            var ev = new Event { Venue = null! };
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void SetEndDate_BeforeStartDate_ShouldThrow()
        {
            var ev = new Event
            {
                StartDate = DateTime.UtcNow.AddDays(2),
                EndDate = DateTime.UtcNow.AddDays(1),
            };
        }
    }
}
