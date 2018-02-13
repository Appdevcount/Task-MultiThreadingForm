using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using IDEX.DAL.Infrastructure;
using IDEX.DAL.DBEntities;
using IDEX.IDEXProxy;

namespace IDEX
{
    public partial class IDEX : Form
    {
        public IDEX()
        {
            InitializeComponent();
        }
        CancellationTokenSource cts = new CancellationTokenSource();
        DBTran DBT = new DBTran();
        IEnumerable<integral_smscontentpush_mt_messages> RecentMTContents;
        integral_smscontentpush_mt_messages MTContent;
        IDEXProxyUtil IXUtil = new IDEXProxyUtil();
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private void IDEX_Load(object sender, EventArgs e)
        {
            var token = cts.Token;
            Task.Factory.StartNew((a) => { ServiceContentHandler(token); }, token);

            DisplayContentStatTimer.Start();
            DisplayContentStatTimer.Interval = 2000; //2 seconds
            DisplayContentStatTimer.Tick += DisplayContentStatTimer_Tick;

            PendingCheckAlertTimer.Start();
            PendingCheckAlertTimer.Interval = 60000*3; //3 hrs
            PendingCheckAlertTimer.Tick += PendingCheckAlertTimer_Tick;

        }

        private void PendingCheckAlertTimer_Tick(object sender, EventArgs e)
        {
            AlertTran.AlertSMS(DBT.GetAllUnProcessedMT().Count().ToString()+" Service Contents Remaining");
        }

        private void DisplayContentStatTimer_Tick(object sender, EventArgs e)
        {
            FillGrid();
            PendingServiceContentStatus.Text = "Pending Service Contents to be processed = "+ DBT.GetAllUnProcessedMT().Count().ToString();
        }

        private void FillGrid()
        {
            ContentStatusdataGridView.DataSource= DBT.GetAllUnProcessedMT();
        }

        public void ServiceContentHandler(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                try
                {
                    RecentMTContents = DBT.GetFewUnProcessedMT();
                }
                catch(Exception ex)
                {
                    log.Error(ex.Message+" "+(ex.InnerException!=null?ex.InnerException.ToString():"") + " " + " Failing to Fetch Data, Some Issue at the Database Interaction");
                    //MessageBox.Show(ex.Message + "\n" + " Failed to Fetch Data, Some Issue at the Database Interaction");
                    AlertTran.AlertSMS("IDEX- Issue in DB Fetch operation");
                }
                string IDEXStatus;
                //IDEXStatus = IXUtil.ServiceContentPush(LinkID: "1212", ChannelID: "1205", Message: "TEST");
                foreach (integral_smscontentpush_mt_messages MT in RecentMTContents)
                {
                    IDEXStatus = IXUtil.ServiceContentPush(LinkID: MT.id.ToString(), ChannelID: MT.category_ids, Message: MT.message);
                    MT.flag = IDEXStatus;
                    MT.process_date = DateTime.Now;
                    try
                    {
                        DBT.UpdateMTStatus(MT);
                    }
                    catch(Exception ex) //New transaction is not allowed because there are other threads running in the session.- Cant be saved from inside foreach, need to do a ToList() on it to disconnect it from the database before running your loop
                    {
                        log.Error(ex.Message + " " + (ex.InnerException != null ? ex.InnerException.ToString() : "") + " while processing Record with ID= " + MT.id);
                        //MessageBox.Show(ex.Message + "\n" + " at Record = " + MT.id );
                        AlertTran.AlertSMS("IDEX- Issue in DB Update operation");
                    }
                }
            }

        }

        private void IDEX_FormClosed(object sender, FormClosedEventArgs e)
        {
            cts.Cancel();
        }

        private void IDEX_FormClosing(object sender, FormClosingEventArgs e)
        {
            cts.Cancel();
        }

        private void PendingServiceContentStatus_Click(object sender, EventArgs e)
        {

        }
    }
}
