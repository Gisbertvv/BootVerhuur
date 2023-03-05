using BootVerhuurWpf;

namespace TestProject1
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        [Apartment(ApartmentState.STA)]
        public void TestGetLogin()
        {


            GetLogin();
            // Arrange
            var sut = new LoginController(); // replace YourClassName with the actual class name
            var usernameOrEmail = "testuser";
            var password = "testpassword";

            // Act
            var result = sut.GetLogin(usernameOrEmail, password);

            // Assert
            Assert.IsTrue(result); // or Assert.IsFalse(result) if you expect the result to be false
            Assert.AreEqual("expected_id", sut.id); // replace "expected_id" with the expected user ID
            Assert.AreEqual("expected_first_name", sut.firstName); // replace "expected_first_name" with the expected first name
                                                                   // add more Assert statements for other properties that should be set after a successful login
        }

    }
}