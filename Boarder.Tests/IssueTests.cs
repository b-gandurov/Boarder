using Boarder.Models;
using Microsoft.VisualBasic;
using System;
using System.Net.NetworkInformation;
using System.Text.RegularExpressions;
using static Boarder.Tests.Helpers.TestData;
using static Boarder.Tests.IssueTests;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Boarder.Tests.Helpers;
using Task = Boarder.Models.Task;


namespace Boarder.Tests
{
    [TestClass]
    public class IssueTests
    {
        [TestMethod]
        public void Issue_Constructor_Assigns_Values_Correctly()
        {
            // Act
            Issue issue = new Issue(BoardItemData.ValidName, IssueData.ValidDescription, BoardItemData.ValidDate);

            // Assert
            Assert.AreEqual(BoardItemData.ValidName, issue.Title);
            Assert.AreEqual(IssueData.ValidDescription, issue.Description);
            Assert.AreEqual(IssueData.InitialStatus, issue.Status);
        }

        [TestMethod]
        public void Issue_Constructor_Assigns_NoDescription_when_Null()
        {

            // Act
            Issue issue = new(BoardItemData.ValidName, null, BoardItemData.ValidDate);

            // Assert
            Assert.AreEqual(BoardItemData.ValidName, issue.Title);
            Assert.AreEqual("No desciption", issue.Description);
            Assert.AreEqual(IssueData.InitialStatus, issue.Status);
        }

        [TestMethod]
        public void Issue_Constructor_ThrowsException_when_Title_is_Null()
        {
            // Arrange
            string title = null;
            // Act

            // Assert
            Assert.ThrowsException<ArgumentException>(() => new Issue(title, IssueData.ValidDescription, BoardItemData.ValidDate));
        }

        [TestMethod]
        public void Issue_Constructor_ThrowsException_when_Title_is_Invalid()
        {
            // Assert
            Assert.ThrowsException<ArgumentException>(() => new Issue(BoardItemData.InvalidNameMin, IssueData.ValidDescription, BoardItemData.ValidDate));
        }

        [TestMethod]
        public void Issue_Constructor_ThrowsException_when_DueDate_Invalid()
        {
            // Assert
            Assert.ThrowsException<ArgumentException>(() => new Issue(BoardItemData.ValidName, IssueData.ValidDescription, BoardItemData.InvalidDate));
        }

        [TestMethod]
        public void Issue_AdvanceStatus_Sets_Status_Verified()
        {

            // Act
            Issue issue = new(BoardItemData.ValidName, IssueData.ValidDescription, BoardItemData.ValidDate);
            issue.AdvanceStatus();

            // Assert
            Assert.AreEqual(Status.Verified, issue.Status);
        }

        [TestMethod]
        public void Issue_AdvanceStatus_Should_Remain_Verified()
        {
            // Arrange
            Issue issue = new Issue(BoardItemData.ValidName, IssueData.ValidDescription, BoardItemData.ValidDate);

            // Act
            issue.AdvanceStatus();
            issue.AdvanceStatus();
            issue.AdvanceStatus();

            // Assert
            Assert.AreEqual(IssueData.EndStatus, issue.Status);
        }

        [TestMethod]
        public void Issue_ReverseStatus_Sets_Status_Open()
        {
            // Arrange
            Issue issue = new Issue(BoardItemData.ValidName, IssueData.ValidDescription, BoardItemData.ValidDate);
            // Act
            issue.AdvanceStatus();
            issue.RevertStatus();

            // Assert
            Assert.AreEqual(Status.Open, issue.Status);
        }

        [TestMethod]
        public void Issue_ReverseStatus_Should_Remain_Open()
        {
            // Arrange
            Issue issue = new Issue(BoardItemData.ValidName, IssueData.ValidDescription, BoardItemData.ValidDate);
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
            string result = $"Issue: '{BoardItemData.ValidName}', [Open|{(BoardItemData.ValidDate).ToString(DateFormat)}] Description: {IssueData.ValidDescription}";

            // Act
            Issue issue = new Issue(BoardItemData.ValidName, IssueData.ValidDescription, BoardItemData.ValidDate);

            // Assert
            Assert.AreEqual(result, issue.ViewInfo());
        }

        [TestMethod]
        public void Issue_Should_Change_Title_When_Valid_Title()
        {
            // Arrange
            Issue issue = new Issue(BoardItemData.ValidName, IssueData.ValidDescription, BoardItemData.ValidDate);
            string newTitle = new string(BoardItemData.ValidName + 1);

            // Act
            issue.Title = newTitle;

            // Assert
            Assert.AreEqual(newTitle, issue.Title);
        }
        [TestMethod]
        public void Issue_Should_Change_DueDate_When_Valid_DueDate()
        {
            // Arrange
            Issue issue = new Issue(BoardItemData.ValidName, IssueData.ValidDescription, BoardItemData.ValidDate);
            DateTime newDate = BoardItemData.ValidDate.AddDays(1);

            // Act
            issue.DueDate = newDate;

            // Assert
            Assert.AreEqual(newDate, issue.DueDate);
        }


        [TestMethod]
        public void Issue_Should_Show_History_when_View_History_is_Called()
        {
            const string DateFormat = "dd-MM-yyyy";
            string expectedStr = $"Created Issue: Issue: '{BoardItemData.ValidName}', [{IssueData.InitialStatus}|{BoardItemData.ValidDate.ToString(DateFormat)}] Description: {IssueData.ValidDescription}. Description: {IssueData.ValidDescription}";
            // Arrange
            Issue issue = new Issue(BoardItemData.ValidName, IssueData.ValidDescription, BoardItemData.ValidDate);
            // Act

            // Assert
            Assert.AreEqual(expectedStr, AdjustTestDataHelper.RemoveTimestamp(issue.ViewHistory()));
        }





        [TestMethod]
        public void Issue_Constructor_Should_Create_Initial_Create_Log()
        {
            const string DateFormat = "dd-MM-yyyy";
            // Arrange
            string result = $"Created Issue: Issue: '{BoardItemData.ValidName}', [{IssueData.InitialStatus}|{BoardItemData.ValidDate.ToString(DateFormat)}] Description: {IssueData.ValidDescription}. Description: {IssueData.ValidDescription}";

            // Act
            Issue issue = new Issue(BoardItemData.ValidName, IssueData.ValidDescription, BoardItemData.ValidDate);

            // Assert
            Assert.AreEqual(result, AdjustTestDataHelper.RemoveTimestamp(issue.ViewHistory()));
        }




    }
}