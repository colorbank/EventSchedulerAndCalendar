using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CalendarEvent.Utinity
{
    public class AppConfig
    {
        public static string GetDBConnection(string connectionDBName)
        {
            try
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true).Build();
                var conn = configuration.GetSection("Data:" + connectionDBName + ":ConnectionString");
                return conn.Value.ToString();
            }
            catch (Exception ex)
            {
                throw new Exception("ErrorConfiguration at " + connectionDBName + " | Detail" + ex);
            }
        }
    }
}
