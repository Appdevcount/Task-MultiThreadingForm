using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDEX.IDEXProxy
{
    public class IDEXProxyUtil
    {
        string User, Password;
        IDEXProxyWS.Content IX = new IDEXProxyWS.Content();
        public IDEXProxyUtil()
        {
            try
            {
                User = System.Configuration.ConfigurationSettings.AppSettings["IdexUser"];
                Password = System.Configuration.ConfigurationSettings.AppSettings["IdexPwd"];
            }
            catch (Exception ex)
            {
                log.Warn(ex.Message + " " + (ex.InnerException != null ? ex.InnerException.ToString() : "") + " Issue in Fetching IDEX Proxy service Credentials");
            }

        }

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public string ServiceContentPush(string LinkID, string ChannelID, string Message)
        {
            string IDEXStatus;
            try
            {
                string EncodedData = DataEncoder.EncodedFinalData(Message);
                //Testing ChannelID= 1224 for IDEX
                //IDEXProxyWS.Result Result = IX.UploadNormalText(User, Password, LinkID, "1224", EncodedData);
                IDEXProxyWS.Result Result = IX.UploadNormalText(User, Password, LinkID, ChannelID, EncodedData);

                IDEXStatus = Result.Code + "," + Result.Description + "," + Result.MAPID;
                return IDEXStatus;
            }
            catch (Exception ex)
            {
                log.Error(ex.Message + " " + (ex.InnerException != null ? ex.InnerException.ToString() : "") + " " + " Issue in accessing IDEX Content Upload Proxy method for the values  LinkID=" + LinkID + ", ChannelID=" + ChannelID + ", Data=" + Message);

                return ex.Message.Substring(0, ex.Message.Length < 30 ? ex.Message.Length : 30);
            }
        }
    }
    public class DataEncoder
    {
        public static byte[] UTF8Encoder(string Message)
        {
            byte[] byteContent = Encoding.UTF8.GetBytes(Message);
            return byteContent;
        }
        public static string Base64Encoder(byte[] ContentbyteArray)
        {
            string B64EncodedSContent = Convert.ToBase64String(ContentbyteArray);
            return B64EncodedSContent;
        }
        public static string EncodedFinalData(string Message)
        {
            byte[] byteContent = UTF8Encoder(Message);
            string B64EncodedSContent = Base64Encoder(byteContent);
            return B64EncodedSContent;
        }
    }
}
