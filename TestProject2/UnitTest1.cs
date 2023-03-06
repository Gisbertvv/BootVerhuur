using BootVerhuurWpf;

namespace TestProject2
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void WrongLogin_Should_Return_False_When_Succesful()
        {
            // Arrange
            var test = new LoginController(); // replace YourClassName with the actual class name
            var usernameOrEmail = "testuser";
            var password = "testpassword";

            // Act
            var result = test.GetLogin(usernameOrEmail, password);

            // Assert
            Assert.IsFalse(result); // or Assert.IsFalse(result) if you expect the result to be false
            
        }
        [Test]
        public void CorrectLogin_Should_Return_True_When_Succesful()
        {
            // Arrange
            var test = new LoginController(); // replace YourClassName with the actual class name
            var usernameOrEmail = "PAUL";
            var password = "W!elkom01";

            // Act
            var result = test.GetLogin(usernameOrEmail, password);

            // Assert
            Assert.IsTrue(result); // or Assert.IsFalse(result) if you expect the result to be false
        }
    }
}