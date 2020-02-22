using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace CalendarEvent.Controllers.Tests
{
    [TestClass()]
    public class CalendarControllerTests
    {

        [TestMethod()]
        public void InserTest()
        {
            try
            {
                var target = new CalendarEvent.Controllers.CalendarController();
                var data = new CalendarEvent.Models.EventSchedulerViewModel();

                data.StrEventdate = CalendarEvent.Utinity.TimeUtility.ConvertDateToString(System.DateTime.Now);
                data.Title = "";
                data.Description = "";
                data.Userid = "Taweechokp";

                var actual = target.Save("insert", data);
                Assert.IsNotNull(actual);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        [TestMethod()]
        public void UpdateTest()
        {
            try
            {
                var target = new  CalendarEvent.Controllers.CalendarController();
                var data = new CalendarEvent.Models.EventSchedulerViewModel();
                data.Eventid = 10;
                data.StrEventdate = "06/02/2020";
                data.Title = "Test Title update";
                data.Description = "Test update description.";
                data.Userid = "Taweechokp";

                var actual = target.Save("update", data);
                Assert.IsNotNull(actual);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        [TestMethod()]
        public void DeleteTest()
        {
            try
            {
                var target = new  CalendarEvent.Controllers.CalendarController();
                var data = new CalendarEvent.Models.EventSchedulerViewModel();
                data.Eventid = 10;

                var actual = target.Delete(data);
                Assert.IsNotNull(actual);
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}