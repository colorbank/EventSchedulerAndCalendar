using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CalendarEvent.Controllers
{
    public class CalendarController : Controller
    {
        public IActionResult Index()
        {
            try
            {
                var viewModel = new CalendarEvent.Models.EventSchedulerViewModel();
                return View("~/Views/Calendar/Index.cshtml",viewModel);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IActionResult ShowMonthList(CalendarEvent.Models.EventSchedulerViewModel param)
        {
            #region Coed
            try
            {
                //Set default parameter.
                if (param.MonthInt == 0) { param.MonthInt = System.DateTime.Now.Month; }
                if (param.YearInt == 0) { param.YearInt = System.DateTime.Now.Year; }

                //Instance model for display.
                var ViewModel = new CalendarEvent.Models.EventSchedulerViewModel();
                ViewModel.DataLists = new List<Models.EventSchedulerViewModel>();
                var DataRow = new CalendarEvent.Models.EventSchedulerViewModel();
                var DataColumn = new CalendarEvent.Models.EventSchedulerViewModel();
                var DataStampList = new List<Models.EventSchedulerViewModel>();

                //Get start date.
                int StartDayOfWeek = (int)(new DateTime(param.YearInt, param.MonthInt, 1).DayOfWeek);
                //Get day of month.
                int DayOfMonth = DateTime.DaysInMonth(param.YearInt, param.MonthInt);

                //Running number of month.
                int RunningDay = 1;

                #region Get Data form DB.
                #region connection string
                var optionsBuilder = new DbContextOptionsBuilder<CalendarEvent.Models.EF.EventcalendarContext>();
                optionsBuilder.UseSqlServer(CalendarEvent.Utility.AppConfig.GetDBConnection("Sample_ConnectionString"));
                CalendarEvent.Models.EF.EventcalendarContext MyContext = new CalendarEvent.Models.EF.EventcalendarContext(optionsBuilder.Options);
                #endregion
                //Query and filter of this month.
                IQueryable<CalendarEvent.Models.EF.EventScheduler> IEventScheduler = MyContext.EventScheduler;
                DateTime StartDate = new DateTime(param.YearInt, param.MonthInt, 1);
                DateTime EndDate = new DateTime(param.YearInt, param.MonthInt, DayOfMonth);
                var efList = IEventScheduler.Where(m => m.Eventdate >= StartDate && m.Eventdate <= EndDate).ToList();

                //Mapping data to list.
                foreach (var item in efList)
                {
                    DataStampList.Add(new Models.EventSchedulerViewModel()
                    {
                        ef = item,
                    });
                }
                #endregion

                //Create rows.
                for (int r = 0; r < 5; r++)
                {
                    //New instance for add column.
                    DataRow = new CalendarEvent.Models.EventSchedulerViewModel();
                    DataRow.DataLists = new List<Models.EventSchedulerViewModel>();

                    //Create colums.
                    for (int c = 0; c < 7; c++)
                    {
                        //Instance new column.
                        DataColumn = new CalendarEvent.Models.EventSchedulerViewModel
                        {
                            //Add data exp.Sun mon tue....
                            dayOfWeek = (DayOfWeek)c
                        };

                        if (r == 0 && c == StartDayOfWeek)//Check start of first row.
                        {
                            DataColumn.DisplayDay = new DateTime(param.YearInt, param.MonthInt, RunningDay);

                            //Stamp data to this date.
                            if (DataStampList.Where(m => m.ef.Eventdate == DataColumn.DisplayDay).Count() > 0)
                            {
                                DataColumn.DataLists = DataStampList.Where(m => m.ef.Eventdate == DataColumn.DisplayDay).ToList();
                            }
                            RunningDay++;
                        }
                        else if (RunningDay > 1 && RunningDay <= DayOfMonth)
                        {
                            DataColumn.DisplayDay = new DateTime(param.YearInt, param.MonthInt, RunningDay);

                            //Stamp data to this date.
                            if (DataStampList.Where(m => m.ef.Eventdate == DataColumn.DisplayDay).Count() > 0)
                            {
                                DataColumn.DataLists = DataStampList.Where(m => m.ef.Eventdate == DataColumn.DisplayDay).ToList();
                            }
                            RunningDay++;
                        }

                        //Add column to row.
                        DataRow.DataLists.Add(DataColumn);
                    }

                    //Add row model to data all.
                    ViewModel.DataLists.Add(DataRow);
                }
                return PartialView("~/Views/Calendar/ShowMonthList.cshtml", ViewModel);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            #endregion
        }



        [HttpPost]
        public JsonResult Save(string actionType, CalendarEvent.Models.EventSchedulerViewModel data)
        {
            #region Code

            #region connection string
            var optionsBuilder = new DbContextOptionsBuilder<CalendarEvent.Models.EF.EventcalendarContext>();
            optionsBuilder.UseSqlServer(CalendarEvent.Utility.AppConfig.GetDBConnection("Sample_ConnectionString"));
            CalendarEvent.Models.EF.EventcalendarContext MyContext = new CalendarEvent.Models.EF.EventcalendarContext(optionsBuilder.Options);
            #endregion

            try
            {
                #region Valdate data.
                if (string.IsNullOrWhiteSpace(data.Userid)) { throw new Exception("System requier UserID!"); }
                if (string.IsNullOrWhiteSpace(data.StrEventdate)) { throw new Exception("System requier StrEventdate!"); }
                if (actionType == "update")
                {
                    if (data.Eventid == 0) { throw new Exception("System requier EventID!"); }
                }
                #endregion

                var CurrentDateTime = CalendarEvent.Utility.TimeUtility.ConvertDateTimeBySysFormate(System.DateTime.Now);
                if (actionType == "insert")
                {
                    #region Insert process.
                    //Initial data.
                    data.ef = new Models.EF.EventScheduler();
                    data.ef.Eventid = 0;
                    data.ef.Userid = data.Userid;
                    data.ef.Eventdate = CalendarEvent.Utility.TimeUtility.ConvertStringToDateBySysFormate(data.StrEventdate);
                    data.ef.Title = data.Title;
                    data.ef.Description = data.Description;
                    data.ef.CreatedDate = CurrentDateTime;
                    data.ef.CreateBy = data.Userid;
                    data.ef.UpdatedDate = CurrentDateTime;
                    data.ef.UpdatedBy = data.Userid;

                    //Add data before Insert.
                    MyContext.Add<CalendarEvent.Models.EF.EventScheduler>(data.ef);
                    #endregion
                }
                else if (actionType == "update")
                {
                    #region Update process.
                    //Query for get this event.
                    var oldData = MyContext.EventScheduler.Where(m => m.Eventid == data.Eventid).FirstOrDefault();
                    if (oldData == null) { throw new Exception("Not exist data!"); }

                    //Initial data.
                    oldData.Title = data.Title;
                    oldData.Description = data.Description;
                    oldData.UpdatedDate = CurrentDateTime;
                    oldData.UpdatedBy = data.Userid;

                    //Add data before update.
                    MyContext.Update<CalendarEvent.Models.EF.EventScheduler>(oldData);
                    #endregion
                }

                //Commit data.
                MyContext.SaveChanges();

                return Json(new { result = true, error = "" });
            }
            catch (Exception ex)
            {
                return Json(new { result = false, error = ex.Message });
            }
            finally
            {
                MyContext.Database.CloseConnection();
            }
            #endregion

        }

        public JsonResult Delete(CalendarEvent.Models.EventSchedulerViewModel data)
        {
            #region Code
            #region connection string
            var optionsBuilder = new DbContextOptionsBuilder<CalendarEvent.Models.EF.EventcalendarContext>();
            optionsBuilder.UseSqlServer(CalendarEvent.Utility.AppConfig.GetDBConnection("Sample_ConnectionString"));
            CalendarEvent.Models.EF.EventcalendarContext MyContext = new CalendarEvent.Models.EF.EventcalendarContext(optionsBuilder.Options);
            #endregion

            try
            {
                //Validate data
                if (data.Eventid == 0) { throw new Exception("System require EventID!"); }


                var oldData = MyContext.EventScheduler.Where(m => m.Eventid == data.Eventid).FirstOrDefault();
                if (oldData == null)
                {
                    throw new Exception("Not exist data event id=" + data.Eventid);
                }

                MyContext.Remove<CalendarEvent.Models.EF.EventScheduler>(oldData);
                MyContext.SaveChanges();


                return Json(new { result = true, error = "" });
            }
            catch (Exception ex)
            {
                return Json(new { result = false, error = ex.Message });
            }
            #endregion
        }
    }
}