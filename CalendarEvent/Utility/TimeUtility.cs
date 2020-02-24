using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace CalendarEvent.Utility
{
    public static class TimeUtility
    {
        public static string ConvertDateToString(this DateTime dateTime)
        {
            string strDateTime = ConvertDateTimeBySysFormate(dateTime).ToString("dd/MM/yyyy");
            strDateTime = strDateTime.Replace('-', '/');
            return strDateTime;
        }
        public static string ConvertDateToString(this DateTime? dateTime)
        {
            if (dateTime != null)
            {
                DateTime thisDate = (DateTime)dateTime;
                string strDateTime = ConvertDateTimeBySysFormate(thisDate).ToString("dd/MM/yyyy");
                strDateTime = strDateTime.Replace('-', '/');
                return strDateTime;
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// formate date Ex: 16th May 2019
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static string ConvertLongDateSuffixToString(this DateTime? dateTime)
        {
            if (dateTime == null)
            {
                return "";
            }
            DateTime thisDate = (DateTime)ConvertDateTimeBySysFormate(dateTime);
            //get suffix
            string suffix = (thisDate.Day % 10 == 1 && thisDate.Day != 11) ? "st" : (thisDate.Day % 10 == 2 && thisDate.Day != 12) ? "nd" : (thisDate.Day % 10 == 3 && thisDate.Day != 13) ? "rd" : "th";
            string strDateTime = "";
            string day = thisDate.ToString("dd");
            string month = thisDate.ToString("MMMM");
            string year = thisDate.ToString("yyyy");
            strDateTime = day + "" + suffix + " " + month + " " + year;
            return strDateTime;
        }
        public static string ConvertDateTimeToString(this DateTime dateTime)
        {
            DateTime thisDate = (DateTime)dateTime;
            //string strDateTime = ConvertDateTimeBySysFormate(dateTime).ToString("dd/MM/yyyy hh:mm:ss tt");
            string strDateTime = ConvertDateTimeBySysFormate(dateTime).ToString("dd/MM/yyyy HH:mm:ss");
            strDateTime = strDateTime.Replace('-', '/');
            return strDateTime;
        }
        public static string ConvertDateTimeToString(this DateTime? dateTime)
        {
            if (dateTime != null)
            {
                DateTime thisDate = (DateTime)dateTime;
                //string strDateTime = ConvertDateTimeBySysFormate(thisDate).ToString("dd/MM/yyyy hh:mm:ss tt");
                string strDateTime = ConvertDateTimeBySysFormate(thisDate).ToString("dd/MM/yyyy HH:mm:ss");
                strDateTime = strDateTime.Replace('-', '/');
                return strDateTime;
            }
            else
            {
                return "";
            }
        }

        public static DateTime ConvertDateTimeBySysFormate(this DateTime dateTime)
        {
            DateTime result = new DateTime();
            //result = Convert.ToDateTime(dateTime, new System.Globalization.CultureInfo("en-US"));
            result = Convert.ToDateTime(dateTime, new System.Globalization.CultureInfo("en-GB"));
            return result;
        }

        public static DateTime? ConvertDateTimeBySysFormate(this DateTime? dateTime)
        {
            DateTime result = new DateTime();
            if (dateTime != null)
            {

                result = Convert.ToDateTime(dateTime, new System.Globalization.CultureInfo("en-GB"));
                return result;
            }
            else
            {
                return null;
            }
        }

        public static DateTime ConvertStringToDateBySysFormate(this string dateTime)
        {
            try
            {
                if (!string.IsNullOrEmpty(dateTime))
                {
                    return DateTime.ParseExact(dateTime, "dd/MM/yyyy", new CultureInfo("en-GB"));
                }
                else
                {
                    return CalendarEvent.Utility.TimeUtility.ConvertDateTimeBySysFormate(DateTime.Now);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static DateTime? ConvertStringToDateNullable(this string dateTime)
        {
            try
            {
                if (!string.IsNullOrEmpty(dateTime))
                {
                    return DateTime.ParseExact(dateTime, "dd/MM/yyyy", new CultureInfo("en-GB"));
                }
                else
                {
                    return new Nullable<DateTime>();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static DateTime ConvertStringToDateTimeBySysFormate(this string dateTime)
        {
            try
            {
                return DateTime.ParseExact(dateTime, "dd/MM/yyyy HH:mm:ss", new CultureInfo("en-GB"));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public static DateTime GetMaxTimeInDate(this DateTime dateTime)
        {
            TimeSpan ts = new TimeSpan(23, 59, 59);
            return dateTime.Date.Add(ts);
        }
        public static DateTime GetMinTimeInDate(this DateTime dateTime)
        {
            TimeSpan ts = new TimeSpan(00, 00, 00);
            return dateTime.Date.Add(ts);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="timestamp"></param>
        /// <returns></returns>
        public static DateTime ConvertFromUnixTimestamp(double timestamp)
        {
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return origin.AddSeconds(timestamp);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static double ConvertToUnixTimestamp(DateTime date)
        {
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            TimeSpan diff = date - origin;
            return Math.Floor(diff.TotalSeconds);
        }

        /// <summary>
        /// คำนวนหาจำนวนวันที่
        /// </summary>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public static double DateDiff(string beginDate, string endDate)
        {
            try
            {
                if (!string.IsNullOrEmpty(beginDate) && !string.IsNullOrEmpty(endDate))
                {
                    DateTime beginDate1 = CalendarEvent.Utility.TimeUtility.ConvertStringToDateBySysFormate(beginDate);
                    DateTime endDate1 = CalendarEvent.Utility.TimeUtility.ConvertStringToDateBySysFormate(endDate);
                    double result = 0;
                    result = DateDiff(beginDate1, endDate1);
                    return result;
                }
                else
                {
                    throw new Exception("require beginDate and endDate");
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// <summary>
        /// คำนวนหาจำนวนวันที่
        /// </summary>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public static double DateDiff(DateTime beginDate, DateTime endDate)
        {
            try
            {
                double result = 0;
                result = (endDate - beginDate).TotalDays;
                return result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="addMonth">**0 is current Date</param>
        /// <param name="isFullName"></param>
        /// <returns></returns>
        public static string GetCurrentMonthName(int addMonth, bool? isFullName)
        {
            try
            {
                System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo("en-GB");
                string MonthName = "";
                if (isFullName == true)
                {
                    MonthName = System.DateTime.Now.AddMonths(addMonth).ToString("MMMM", culture);
                }
                else
                {
                    MonthName = System.DateTime.Now.AddMonths(addMonth).ToString("MMM", culture);
                }
                return MonthName;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="addMonth">**0 is current Date</param>
        /// <param name="isFullName"></param>
        /// <returns></returns>
        public static string GetCurrentMonthYearName(int addMonth, bool? isFullName)
        {
            try
            {
                System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo("en-GB");
                string MonthName = "";
                if (isFullName == true)
                {
                    MonthName = System.DateTime.Now.AddMonths(addMonth).ToString("MMMM", culture) + "-" + System.DateTime.Now.AddMonths(addMonth).ToString("yyyy", culture);
                }
                else
                {
                    MonthName = System.DateTime.Now.AddMonths(addMonth).ToString("MMM", culture) + "-" + System.DateTime.Now.AddMonths(addMonth).ToString("yy", culture);
                }
                return MonthName;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// get first date of weekly
        /// หาข้อมูล วันที่เริ่มต้นของแต่ละสัปดาห์ ใน เดือนนั้นๆ
        /// </summary>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public static DateTime GetFirstDateOfWeekly(this DateTime endDate)
        {
            try
            {
                DateTime result = new DateTime();
                int delta = 0;
                if (endDate.DayOfWeek == DayOfWeek.Sunday)
                {
                    delta = (-6);
                }
                else
                {
                    delta = DayOfWeek.Monday - endDate.DayOfWeek;
                }
                DateTime monday = endDate.AddDays(delta);
                if (monday.Month == endDate.Month)
                {
                    result = monday;

                }
                else
                {
                    result = new DateTime(endDate.Year, endDate.Month, 1);
                }

                return result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// <summary>
        /// get first date of monthly
        /// </summary>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public static DateTime GetFirstDateOfMonthly(this DateTime endDate)
        {
            try
            {
                DateTime result = new DateTime();
                result = new DateTime(endDate.Year, endDate.Month, 1);
                return result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// get first date of yearly
        /// </summary>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public static DateTime GetFirstDateOfYearly(this DateTime endDate)
        {
            try
            {
                DateTime result = new DateTime();
                result = new DateTime(endDate.Year, 1, 1);
                return result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static String GetDateTimeForNameOfFile(this DateTime dateTime)
        {
            try
            {
                String result = "";
                result = dateTime.Day.ToString("D2") + dateTime.Month.ToString("D2") + dateTime.Year.ToString("D2") + "_" + dateTime.Hour.ToString("D2") + "" + dateTime.Minute.ToString("D2");
                return result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static String GetDateFileForNameOfFile(this DateTime dateTime)
        {
            try
            {
                String result = "";
                result = dateTime.Day.ToString("D2") + dateTime.Month.ToString("D2") + dateTime.Year.ToString("D2");
                return result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Get List of date from calendar by month and year
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        public static List<DateTime> GetDateFromCalendarByMonth(int year, int month)
        {
            try
            {
                List<DateTime> resultList = new List<DateTime>();
                int amountDaysOfMonty = System.DateTime.DaysInMonth(year, month);
                // Uses the default calendar of the InvariantCulture.
                CultureInfo en = CultureInfo.CreateSpecificCulture("en-GB");
                //CultureInfo en = CultureInfo.CreateSpecificCulture("th-TH");
                Calendar calendar = en.Calendar;
                for (int i = 1; i <= amountDaysOfMonty; i++)
                {
                    resultList.Add(new DateTime(year, month, i, calendar));
                }
                return resultList;
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public static string Convert12h_TO_24h(string am_pm)
        {
            try
            {
                DateTime dt;
                bool res = DateTime.TryParse(am_pm, out dt);
                return dt.ToString("HH:mm");
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static string GetString_HoursAndMinute_12h(this DateTime date)
        {
            try
            {
                string time = "";
                time = date.ToString("hh:mm");
                int hour = date.Hour;
                if (hour <= 12)
                {
                    time = time + " AM ";
                }
                else
                {
                    time = time + " PM ";
                }
                return time;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static DateTime DefineDateTimeFromUI(string date, string time12h)
        {
            try
            {
                DateTime result;
                string time24h = TimeUtility.Convert12h_TO_24h(time12h);
                string strDateTime = date + " " + time24h + ":00";
                result = TimeUtility.ConvertStringToDateTimeBySysFormate(strDateTime);
                return result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }




    }
}
