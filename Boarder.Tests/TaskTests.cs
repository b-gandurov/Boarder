using Boarder.Models;
using Microsoft.VisualBasic;
using System;
using System.Net.NetworkInformation;
using System.Text.RegularExpressions;
using static Boarder.Tests.Helpers.TestData;
using static Boarder.Tests.IssueTests;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Task = Boarder.Models.Task;
using Boarder.Tests.Helpers;


namespace Boarder.Tests
{
    [TestClass]
    public class TaskTests
    {
        [TestMethod]
        public void Task_Constructor_Assigns_Values_Correctly()
        {
            // Act
            Task task = new Task(BoardItemData.ValidName, TaskData.ValidAsignee, BoardItemData.ValidDate);

            // Assert
            Assert.AreEqual(BoardItemData.ValidName, task.Title);
            Assert.AreEqual(TaskData.ValidAsignee, task.Assignee);
            Assert.AreEqual(TaskData.InitialStatus, task.Status);
        }


        [TestMethod]
        public void Task_Constructor_Throws_ArgumentException_When_Asignee_Null()
        {
            string asignee = null;
            // Act

            // Assert
            Assert.ThrowsException<ArgumentException>(() => new Task(BoardItemData.ValidName, asignee, BoardItemData.ValidDate));
        }
        [TestMethod]
        public void Task_Constructor_Throws_ArgumentException_When_Asignee_Short()
        {

            // Assert
            Assert.ThrowsException<ArgumentException>(() => new Task(BoardItemData.ValidName, TaskData.InvalidAsigneeMin, BoardItemData.ValidDate));
        }

        [TestMethod]
        public void Task_Constructor_Should_Change_Asignee_when_Correct()
        {
            // Arrange
            Task task = new(BoardItemData.ValidName, TaskData.ValidAsignee, BoardItemData.ValidDate);
            string newAsignee = new string(TaskData.ValidAsignee + 1);

            // Act
            task.Assignee = newAsignee;

            // Assert
            Assert.AreEqual(newAsignee, task.Assignee);
        }

        [TestMethod]
        public void Task_Constructor_ThrowsException_when_Title_is_Null()
        {
            // Arrange
            string title = null;
            // Act

            // Assert
            Assert.ThrowsException<ArgumentException>(() => new Task(title, TaskData.ValidAsignee, BoardItemData.ValidDate));
        }

        [TestMethod]
        public void Task_Constructor_ThrowsException_when_Title_is_Invalid()
        {
            // Assert
            Assert.ThrowsException<ArgumentException>(() => new Issue(BoardItemData.InvalidNameMax, IssueData.ValidDescription, BoardItemData.ValidDate));
        }

        [TestMethod]
        public void Task_Constructor_ThrowsException_when_DueDate_Invalid()
        {
            // Assert
            Assert.ThrowsException<ArgumentException>(() => new Issue(BoardItemData.ValidName, TaskData.ValidAsignee, BoardItemData.InvalidDate));
        }

        [TestMethod]
        public void Task_Advance_Initial_Status_To_InProgress()
        {

            // Act
            Task task = new(BoardItemData.ValidName, TaskData.ValidAsignee, BoardItemData.ValidDate);
            task.AdvanceStatus();

            // Assert
            Assert.AreEqual(Status.InProgress, task.Status);
        }

        [TestMethod]
        public void Task_AdvanceStatus_Should_Remain_Verified()
        {
            // Arrange
            Task task = new(BoardItemData.ValidName, TaskData.ValidAsignee, BoardItemData.ValidDate);

            // Act
            task.AdvanceStatus();
            task.AdvanceStatus();
            task.AdvanceStatus();
            task.AdvanceStatus();

            // Assert
            Assert.AreEqual(IssueData.EndStatus, task.Status);
        }

        [TestMethod]
        public void Task_ReverseStatus_Sets_Status_Open()
        {
            // Arrange
            Task task = new(BoardItemData.ValidName, TaskData.ValidAsignee, BoardItemData.ValidDate);
            // Act
            task.AdvanceStatus();
            task.RevertStatus();

            // Assert
            Assert.AreEqual(TaskData.InitialStatus, task.Status);
        }

        [TestMethod]
        public void Task_ReverseStatus_Should_Remain_Todo()
        {
            // Arrange
            Task task = new(BoardItemData.ValidName, TaskData.ValidAsignee, BoardItemData.ValidDate);
            // Act
            task.RevertStatus();
            task.RevertStatus();
            task.RevertStatus();

            // Assert
            Assert.AreEqual(TaskData.InitialStatus, task.Status);
        }


        [TestMethod]
        public void Task_ViewInfo_Should_Return_Formated_Description_of_Issue()
        {
            const string DateFormat = "dd-MM-yyyy";
            // Arrange
            string result = $"Task: '{BoardItemData.ValidName}', [{TaskData.InitialStatus}|{(BoardItemData.ValidDate).ToString(DateFormat)}] Assignee: {TaskData.ValidAsignee}";

            // Act
            Task task = new(BoardItemData.ValidName, TaskData.ValidAsignee, BoardItemData.ValidDate);

            // Assert
            Assert.AreEqual(result, task.ViewInfo());
        }

        [TestMethod]
        public void Task_Should_Change_Title_When_Valid_Title()
        {
            // Arrange
            Task task = new(BoardItemData.ValidName, TaskData.ValidAsignee, BoardItemData.ValidDate);
            string newTitle = new string(BoardItemData.ValidName + 1);

            // Act
            task.Title = newTitle;

            // Assert
            Assert.AreEqual(newTitle, task.Title);
        }
        [TestMethod]
        public void Task_Should_Change_DueDate_When_Valid_DueDate()
        {
            // Arrange
            Task task = new(BoardItemData.ValidName, TaskData.ValidAsignee, BoardItemData.ValidDate);
            DateTime newDate = BoardItemData.ValidDate.AddDays(1);

            // Act
            task.DueDate = newDate;

            // Assert
            Assert.AreEqual(newDate, task.DueDate);
        }

        [TestMethod]
        public void Task_Should_Show_History_when_View_History_is_Called()
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
        public void Task_Constructor_Should_Create_Initial_Create_Log()
        {
            const string DateFormat = "dd-MM-yyyy";
            // Arrange
            string result = $"Created Task: Task: '{BoardItemData.ValidName}', [{TaskData.InitialStatus}|{BoardItemData.ValidDate.ToString(DateFormat)}] Assignee: {TaskData.ValidAsignee}";

            // Act
            Task task = new (BoardItemData.ValidName, TaskData.ValidAsignee, BoardItemData.ValidDate);

            // Assert
            Assert.AreEqual(result, AdjustTestDataHelper.RemoveTimestamp(task.ViewHistory()));
        }




    }
}