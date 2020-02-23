using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CalendarEvent.Models
{
    public class EventSchedulerViewModel
    {
        private CalendarEvent.Models.EF.EventScheduler _ef;

        #region Extanal field.

        public CalendarEvent.Models.EF.EventScheduler ef
        {
            get { return _ef; }
            set { _ef = value; }
        }

        private string _strEventdate;

        public string StrEventdate
        {
            get { return _strEventdate; }
            set { _strEventdate = value; }
        }

        private int _Eventid;

        public int Eventid
        {
            get { return _Eventid; }
            set { _Eventid = value; }
        }

        private string _Title;

        public string Title
        {
            get { return _Title; }
            set
            {
                _Title = value;

                if (string.IsNullOrWhiteSpace(_Title))
                {
                    _Title = "(No title)";
                }

            }
        }

        private string _Description;

        public string Description
        {
            get { return _Description; }
            set
            {
                _Description = value;
                if (string.IsNullOrWhiteSpace(_Description))
                {
                    _Description = string.Empty;
                }

            }
        }

        private string _Userid;

        public string Userid
        {
            get { return _Userid; }
            set { _Userid = value; }
        }

        public DayOfWeek dayOfWeek { get; set; }
        public DateTime? DisplayDay { get; set; }
        #endregion

        /// <summary>
        /// Use for dispay data.
        /// </summary>
        public List<CalendarEvent.Models.EventSchedulerViewModel> DataLists { get; set; }

        #region Parameter filed.
        public int MonthInt { get; set; }
        public int YearInt { get; set; }
        #endregion

    }
}
