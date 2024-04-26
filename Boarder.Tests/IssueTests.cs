using Boarder.Models;
using System.Net.NetworkInformation;


namespace Boarder.Tests
{
    [TestClass]
    public class IssueTests
    {
        [TestMethod]
        public void Issue_Constructor_AssignsValuesCorrectly()
        {
            // Arrange
            string title = "Test Task";
            string description = "Test Description";
            DateTime dueDate = DateTime.Today.AddDays(2);
            Status status = Status.Open;
            // Act
            Issue issue = new Issue(title,description, dueDate);

            // Assert
            Assert.AreEqual(title, issue.Title);
            Assert.AreEqual(description, issue.Description);
            Assert.AreEqual(status, issue.Status);
        }
        [TestMethod]
        public void Issue_Constructor_AssignsNoDescription_when_Null()
        {
            // Arrange
            string title = "Test Task";
            DateTime dueDate = DateTime.Today.AddDays(2);
            Status status = Status.Open;
            // Act
            Issue issue = new Issue(title, null, dueDate);

            // Assert
            Assert.AreEqual(title, issue.Title);
            Assert.AreEqual("No desciption", issue.Description);
            Assert.AreEqual(status, issue.Status);
        }

        [TestMethod]
        public void Issue_AdvanceStatus_SetsStatusToVerified()
        {
            // Arrange
            string title = "Bug Fix";
            string description = "Fixes UI bug";
            DateTime dueDate = DateTime.Today.AddDays(2);

            // Act
            Issue issue = new Issue(title, description, dueDate);
            issue.AdvanceStatus();

            // Assert
            Assert.AreEqual(Status.Verified, issue.Status);
        }

        [TestMethod]
        public void Issue_AdvanceStatus_Should_RemaAtVerified()
        {
            // Arrange
            Issue issue = new Issue("Bug Fix", "Fixes UI bug", DateTime.Today.AddDays(2));

            // Act
            issue.AdvanceStatus();
            issue.AdvanceStatus();
            issue.AdvanceStatus();

            // Assert
            Assert.AreEqual(Status.Verified, issue.Status);
        }

        [TestMethod]
        public void Issue_ReverseStatus_SetsStatusToOpen()
        {
            // Arrange
            Issue issue = new Issue("Bug Fix", "Fixes UI bug", DateTime.Today.AddDays(2));
            // Act
            issue.AdvanceStatus();
            issue.RevertStatus();

            // Assert
            Assert.AreEqual(Status.Open, issue.Status);
        }

        [TestMethod]
        public void Issue_ReverseStatus_Should_RemaAtOpen()
        {
            // Arrange
            Issue issue = new Issue("Bug Fix", "Fixes UI bug", DateTime.Today.AddDays(2));
            // Act
            issue.RevertStatus();
            issue.RevertStatus();
            issue.RevertStatus();

            // Assert
            Assert.AreEqual(Status.Open, issue.Status);
        }
        [TestMethod]
        public void Issue_ViewInfo_Should_Return_Formated_Description_of_Issue()
        {
            const string DateFormat = "dd-MM-yyyy";
            // Arrange
            string result = $"Issue: 'Bug Fix', [Open|{(DateTime.Today.AddDays(2)).ToString(DateFormat)}] Description: Fixes UI bug";

            // Act
            Issue issue = new Issue("Bug Fix", "Fixes UI bug", DateTime.Today.AddDays(2));

            // Assert
            Assert.AreEqual(result, issue.ViewInfo());
        }

    }
}