using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDEX.DAL.DBEntities;

namespace IDEX.DAL.Infrastructure
{
    public class AlertTran
    {

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public static void AlertSMS(string Message)
        {
            pubsEntities dbctx = new pubsEntities();
            try
            {
                int AlertUsersCount = Convert.ToInt16(System.Configuration.ConfigurationSettings.AppSettings["AlertUsers"]);
                string AlertMSISDNKey = "AlertMSISDN";
                List<string> AlertMSISDNs = new List<string>();
                for (int i = 1; i <= AlertUsersCount; i++)
                {
                    string AlertMSISDN = System.Configuration.ConfigurationSettings.AppSettings[AlertMSISDNKey + i.ToString()];
                    AlertMSISDNs.Add(AlertMSISDN);
                }
                foreach (string MSISDN in AlertMSISDNs)
                {
                    dbctx.receives.Add(new receive { prefix = "RG", message = Message, scode = "96900", sender = MSISDN, dates = DateTime.Now.ToShortDateString(), times = DateTime.Now.ToShortTimeString(), flag = "NO" });
                }
                dbctx.SaveChanges();
            }
            catch(Exception ex)
            {
                log.Warn(ex.Message + " " + (ex.InnerException != null ? ex.InnerException.ToString() : "") + " " + " Some Issue at the Database Interaction. Alert SMS not sent!");

            }
        }
    }
}
