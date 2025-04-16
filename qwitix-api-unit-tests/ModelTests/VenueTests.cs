using qwitix_api.Core.Exceptions;
using qwitix_api.Core.Models;

namespace qwitix_api_unit_tests
{
    [TestClass]
    public class VenueTests
    {
        private Venue CreateValidVenue() =>
            new Venue
            {
                Name = "Valid Name",
                Address = "Valid Address",
                City = "Valid City",
            };

        [TestMethod]
        public void CreateVenue_WithValidData_ShouldSucceed()
        {
            var venue = new Venue
            {
                Name = "Concert Hall",
                Address = "123 Main Street",
                City = "New York",
                State = "NY",
                Zip = "12345-6789",
            };

            Assert.AreEqual("Concert Hall", venue.Name);
            Assert.AreEqual("123 Main Street", venue.Address);
            Assert.AreEqual("New York", venue.City);
            Assert.AreEqual("NY", venue.State);
            Assert.AreEqual("12345-6789", venue.Zip);
        }

        [DataTestMethod]
        [DataRow(null)]
        [DataRow("")]
        [DataRow(" ")]
        [ExpectedException(typeof(ValidationException))]
        public void SetName_Invalid_ShouldThrow(string? input)
        {
            var venue = CreateValidVenue();
            venue.Name = input!;
        }

        public static IEnumerable<object[]> InvalidNameData =>
            new List<object[]>
            {
                new object[] { "AB" },
                new object[] { "A" + new string('B', 255) },
            };

        [DataTestMethod]
        [DynamicData(nameof(InvalidNameData))]
        [ExpectedException(typeof(ValidationException))]
        public void SetName_InvalidLength_ShouldThrow(string input)
        {
            var venue = CreateValidVenue();
            venue.Name = input;
        }

        [DataTestMethod]
        [DataRow(null)]
        [DataRow("")]
        [DataRow(" ")]
        [ExpectedException(typeof(ValidationException))]
        public void SetAddress_Invalid_ShouldThrow(string? input)
        {
            var venue = CreateValidVenue();
            venue.Address = input!;
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void SetAddress_TooLong_ShouldThrow()
        {
            var venue = CreateValidVenue();
            venue.Address = new string('A', 256);
        }

        [DataTestMethod]
        [DataRow(null)]
        [DataRow("")]
        [DataRow(" ")]
        [ExpectedException(typeof(ValidationException))]
        public void SetCity_Invalid_ShouldThrow(string? input)
        {
            var venue = CreateValidVenue();
            venue.City = input!;
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void SetCity_TooLong_ShouldThrow()
        {
            var venue = CreateValidVenue();
            venue.City = new string('C', 101);
        }

        [DataTestMethod]
        [DataRow("California")]
        [DataRow(null)]
        public void SetState_ValidValues_ShouldSucceed(string? input)
        {
            var venue = CreateValidVenue();
            venue.State = input;
            Assert.AreEqual(input, venue.State);
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void SetState_TooLong_ShouldThrow()
        {
            var venue = CreateValidVenue();
            venue.State = new string('S', 101);
        }

        [DataTestMethod]
        [DataRow("12345")]
        [DataRow("1234567890")]
        [DataRow("12345-6789")]
        [DataRow(null)]
        public void SetZip_Valid_ShouldSucceed(string? zip)
        {
            var venue = CreateValidVenue();
            venue.Zip = zip;
            Assert.AreEqual(zip, venue.Zip);
        }

        [DataTestMethod]
        [DataRow("abcde")]
        [DataRow("12345-abc")]
        [DataRow("12-34-56")]
        [ExpectedException(typeof(ValidationException))]
        public void SetZip_InvalidFormat_ShouldThrow(string zip)
        {
            var venue = CreateValidVenue();
            venue.Zip = zip;
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void SetZip_TooLong_ShouldThrow()
        {
            var venue = CreateValidVenue();
            venue.Zip = "12345678901";
        }
    }
}
