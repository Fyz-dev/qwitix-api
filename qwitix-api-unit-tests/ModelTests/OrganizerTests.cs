using qwitix_api.Core.Exceptions;
using qwitix_api.Core.Models;

namespace qwitix_api_unit_tests
{
    [TestClass]
    public class OrganizerTests
    {
        public static IEnumerable<object[]> InvalidNameData =>
            new List<object[]>
            {
                new object[] { "" },
                new object[] { "   " },
                new object[] { "A" + new string('a', 255) },
            };

        public static IEnumerable<object[]> ValidBioData =>
            new List<object[]>
            {
                new object[] { "" },
                new object[] { "This is a short bio." },
                new object[] { new string('a', 2500) },
            };

        public static IEnumerable<object[]> ValidNameData =>
            new List<object[]>
            {
                new object[] { "Valid Organizer" },
                new object[] { new string('X', 250) },
            };

        [TestMethod]
        [DataRow(null)]
        [DataRow("")]
        [DataRow("   ")]
        [ExpectedException(typeof(ValidationException))]
        public void SetUserId_Invalid_ThrowsValidationException(string userId)
        {
            var organizer = new Organizer { UserId = userId };
        }

        [TestMethod]
        [DataRow("507f1f77bcf86cd799439011")]
        public void SetUserId_Valid_SetsValue(string userId)
        {
            var organizer = new Organizer { UserId = userId };
            Assert.AreEqual(userId, organizer.UserId);
        }

        [TestMethod]
        [DynamicData(nameof(InvalidNameData))]
        [ExpectedException(typeof(ValidationException))]
        public void SetName_Invalid_ThrowsValidationException(string name)
        {
            var organizer = new Organizer { Name = name };
        }

        [TestMethod]
        [DynamicData(nameof(ValidNameData))]
        public void SetName_Valid_SetsValue(string name)
        {
            var organizer = new Organizer { Name = name };
            Assert.AreEqual(name, organizer.Name);
        }

        [TestMethod]
        [DynamicData(nameof(ValidBioData))]
        public void SetBio_Valid_SetsValue(string bio)
        {
            var organizer = new Organizer { Bio = bio };
            Assert.AreEqual(bio, organizer.Bio);
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void SetBio_TooLong_ThrowsValidationException()
        {
            string longBio = new string('a', 2501);

            var organizer = new Organizer { Bio = longBio };
        }

        [TestMethod]
        [DataRow(null)]
        [DataRow("https://example.com/image.png")]
        [DataRow("ftp://server.com/resource.jpg")]
        public void SetImageUrl_Valid_DoesNotThrow(string? url)
        {
            var organizer = new Organizer { ImageUrl = url };
            Assert.AreEqual(url, organizer.ImageUrl);
        }

        [TestMethod]
        [DataRow("not-a-url")]
        [DataRow("http:/invalid.com")]
        [DataRow("example.com")]
        [ExpectedException(typeof(ValidationException))]
        public void SetImageUrl_Invalid_ThrowsValidationException(string url)
        {
            var organizer = new Organizer { ImageUrl = url };
        }
    }
}
