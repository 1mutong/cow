using DairyCow.BLL;
using DairyCow.Model;
using System;
using System.Collections.Generic;
using System.ServiceProcess;
using System.Timers;

namespace DairyService
{
    public partial class DailyReportService : ServiceBase
    {
        private Timer dailyReportTimer;

        public DailyReportService()
        {
            InitializeComponent();
            dailyReportTimer = new Timer();
            dailyReportTimer.Interval = 3600000.0;//一小时
            dailyReportTimer.AutoReset = true;
            dailyReportTimer.Elapsed += dailyReportTimer_Elapsed;

        }

        void dailyReportTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (DateTime.Now.Hour==23)
            {
                PastureBLL pBLL = new PastureBLL();
                List<Pasture> list = pBLL.GetPastures();
                foreach (Pasture item in list)
                {
                    DailyReportBLL dBLL = new DailyReportBLL(item.ID);
                    dBLL.InsertDailyReport();
                }
            }
        }

        protected override void OnStart(string[] args)
        {
            dailyReportTimer.Start();
        }

        protected override void OnStop()
        {
            dailyReportTimer.Stop();
        }        
    }
}
