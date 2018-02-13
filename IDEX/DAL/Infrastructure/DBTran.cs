using IDEX.DAL.DBEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDEX.DAL.Infrastructure
{
    public class DBTran
    {
        
        public IEnumerable<ServiceContentGridModel> GetAllUnProcessedMT()
        {
            IntegralDBEntities dbctx = new IntegralDBEntities();

            IEnumerable<ServiceContentGridModel> AllUnProcMT= dbctx.integral_smscontentpush_mt_messages.Where(c => c.flag.Equals("NO")).Select(c => new ServiceContentGridModel { ID = c.id.ToString(), ShortCode = c.shortcode, ChannelID = c.category_ids, Message = c.message, TranDate = c.tran_date, Status = c.flag }).ToList<ServiceContentGridModel>();
            return AllUnProcMT;
        }
        public IEnumerable<integral_smscontentpush_mt_messages> GetFewUnProcessedMT()
        {
            IntegralDBEntities dbctx = new IntegralDBEntities();

            //IEnumerable<integral_smscontentpush_mt_messages> UnProcMT = dbctx.integral_smscontentpush_mt_messages.Where(c => c.flag.Equals("True,7735624")).Take(5).ToList<integral_smscontentpush_mt_messages>();
            IEnumerable<integral_smscontentpush_mt_messages> UnProcMT = dbctx.integral_smscontentpush_mt_messages.Where(c => c.flag.Equals("NO")).Take(5).ToList<integral_smscontentpush_mt_messages>();
            return UnProcMT;
        }
        public void UpdateMTStatus(integral_smscontentpush_mt_messages MT)
        {
            IntegralDBEntities dbctx = new IntegralDBEntities();

            dbctx.Entry(MT).State = System.Data.Entity.EntityState.Modified;
            dbctx.SaveChanges();
        }

    }

    public class ServiceContentGridModel
        {
        public string ID { get; set; }
        public string ShortCode { get; set; }
        public string ChannelID { get; set; }
        public string Message { get; set; }
        public DateTime? TranDate { get; set; }
        public string Status { get; set; }

    }
}
