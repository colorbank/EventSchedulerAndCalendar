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

                data.StrEventdate = CalendarEvent.Utility.TimeUtility.ConvertDateToString(System.DateTime.Now);
                data.Title = "Musk Muchii" + CalendarEvent.Utility.TimeUtility.ConvertDateToString(System.DateTime.Now); ;
                data.Description = "";
                data.Userid = "Taweechokp";
                //data.StartAt = new TimeSpan(8, 30, 30);
                //data.EndAt = new TimeSpan(17, 30, 25);

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

        [TestMethod()]
        public void ShowMonthListTest()
        {
            try
            {
                var target = new  CalendarEvent.Controllers.CalendarController();
                var data = new CalendarEvent.Models.EventSchedulerViewModel();
                var actual = target.ShowMonthList(data);
                Assert.IsNotNull(actual);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        [TestMethod()]
        public void ResultSearchTest()
        {
            try
            {
                var target = new  CalendarEvent.Controllers.CalendarController();
                var modelFilter = new CalendarEvent.Models.EventSchedulerViewModel();
                modelFilter.Userid = "Taweechokp";
                modelFilter.TextSearch = "title";
                var actual = target.SearchList(modelFilter);
                Assert.IsNotNull(actual);
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}