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
            return View();
        }

        [HttpPost]
        public JsonResult Save(string actionType,CalendarEvent.Models.EventSchedulerViewModel data)
        {
            #region Code

            #region connection string
            var optionsBuilder = new DbContextOptionsBuilder<CalendarEvent.Models.EF.EventcalendarContext>();
            optionsBuilder.UseSqlServer( CalendarEvent.Utinity.AppConfig.GetDBConnection("Sample_ConnectionString"));
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

                var CurrentDateTime = CalendarEvent.Utinity.TimeUtility.ConvertDateTimeBySysFormate(System.DateTime.Now);
                if (actionType == "insert")
                {
                    //Initial data.
                    data.ef = new Models.EF.EventScheduler();
                    data.ef.Eventid = 0;
                    data.ef.Userid = data.Userid;
                    data.ef.Eventdate = CalendarEvent.Utinity.TimeUtility.ConvertStringToDateBySysFormate(data.StrEventdate);
                    data.ef.Title = data.Title;
                    data.ef.Description = data.Description;
                    data.ef.CreatedDate = CurrentDateTime;
                    data.ef.CreateBy = data.Userid;
                    data.ef.UpdatedDate = CurrentDateTime;
                    data.ef.UpdatedBy = data.Userid;

                    //Add data before Insert.
                    MyContext.Add<CalendarEvent.Models.EF.EventScheduler>(data.ef);
                }
                else
                {
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
    }
}