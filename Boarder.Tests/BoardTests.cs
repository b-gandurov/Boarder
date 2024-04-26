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
using Boarder.Loggers;


namespace Boarder.Tests
{
    [TestClass]
    public class BoardTests
    {
        [TestMethod]
        public void Board_Should_Add_Item_when_No_Items()
        {
            // Act
            Issue issue = new(BoardItemData.ValidName, IssueData.ValidDescription, BoardItemData.ValidDate);
            Board.AddItem(issue);

            // Assert
            Assert.AreEqual(1, Board.TotalItems);
        }

        [TestMethod]
        public void Board_Should_Throw_When_Item_Exists()
        {
            // Act
            Issue issue = new(BoardItemData.ValidName, IssueData.ValidDescription, BoardItemData.ValidDate);
            Board.AddItem(issue);

            // Assert
            Assert.ThrowsException<InvalidOperationException>(() => Board.AddItem(issue));
        }




    }
}