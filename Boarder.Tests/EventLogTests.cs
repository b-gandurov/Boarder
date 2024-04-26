using Boarder.Models;
using System.Net.NetworkInformation;


namespace Boarder.Tests
{
    [TestClass]
    public class EventLogTests
    {
        [TestMethod]
        public void EventLog_Should_Return_Time_and_Description_When_ViewInfo()
        {
            const string dateFormat = "yyyyMMdd|HH:mm:ss.ffff";
            // Arrange
            DateTime dueDate = DateTime.Now.AddDays(2);
            string description = "Test Description";
            string result= $"[{dueDate.ToString(dateFormat)}]{description}";

            // Act
            EventLog log = new EventLog(description);

            // Assert
            Assert.AreEqual(result, log.ViewInfo());
        }

    }
}