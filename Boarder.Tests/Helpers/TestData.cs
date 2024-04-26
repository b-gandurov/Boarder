using Boarder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boarder.Tests.Helpers
{
    public class TestData
    {
        public static class BoardItemData
        {
            public static string ValidName = new string('x', 6);
            public static string InvalidNameMin = new string('x', 4);
            public static string InvalidNameMax = new string('x', 31);
            public static DateTime ValidDate = DateTime.Now.AddDays(1);
            public static DateTime InvalidDate = DateTime.Now.AddDays(-1);
        }

        public static class IssueData
        {
            public static string ValidDescription = new string("xxx");
            public static Status InitialStatus = Status.Open;
            public static Status EndStatus = Status.Verified;
        }

        public static class TaskData
        {
            public static string ValidAsignee = new string('x', 6);
            public static string InvalidAsigneeMin = new string('x', 4);
            public static string InvalidAsigneeMax = new string('x', 31);
            public static Status InitialStatus = Status.Todo;
            public static Status EndStatus = Status.Verified;
        }
    }
}
