using BootVerhuurWpf;

namespace TestProject2
{
    [TestFixture]
    public class Tests
    {
        [TestCase((string)"Damian", "", false)]//existing user no password
        [TestCase((string)"", "", false)]//no username or password
        [TestCase((string)"", "Damian", false)]//only password
        [TestCase((string)"", "W!elkom01", false)]//existing password no user
        [TestCase((string)"DAMIAN", "W1elkom01", false)]//Username without all caps
        [TestCase((string)"PAUL", "Havermout1325!", true)]//Correct login user
        [TestCase((string)"Damian", "W!elkom01", true)]//Correct login admin



        [Test]
        public void Login(string user, string password, bool expected)
        {
            // Arrange
            var test = new LoginController();


            // Act
            var result = test.GetLogin(user, password);

            // Assert
            Assert.AreEqual(expected, result);

        }

    }
}