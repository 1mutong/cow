using Common;
//using CowSite.ServiceReference1;
using DairyCow.BLL;
using DairyCow.Model;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using WCfModel;

namespace CowSite.Controllers.Feed
{
    public class FormulaController : Controller
    {
        private FormulaBLL bllFormula = new FormulaBLL();

        public JobBLL jobBll = new JobBLL();
        public ActionResult Select(string id)
        {
            ViewBag.CowGroupID = id;
            return View("~/Views/Feed/Formula/Select.cshtml");
        }

        public ActionResult Modify(string formulaID)
        {
            ViewBag.FormulaID = formulaID;
            return View("~/Views/Feed/Formula/Modify.cshtml");
        }

        public JsonResult UpdateFormulaOfCowGroup(string formulaID, string cowGroupID)
        {
            this.bllFormula.UpdateFormulaOfCowGroup(formulaID, cowGroupID);
            var result = new
            {
                result = 1
            };
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Process()
        {
            return View("~/Views/Feed/Formula/Process.cshtml");
        }

        public ActionResult FeedList()
        {
            return View("~/Views/Feed/Formula/FeedList.cshtml");
        }

        public ActionResult Analysis()
        {
            //~/Views/Feed/Formula/Analysis.cshtml
            return View("~/Views/Feed/Formula/Analysis.cshtml");
        }

        //Service1Client client = new Service1Client();
        public JsonResult GetFormulaProcess()
        {
            List<JobDetailInfo> feedInfoList = jobBll.GetJobInfo();
            var gridData = new { Rows = feedInfoList, Total = feedInfoList.Count };
            return Json(gridData, JsonRequestBehavior.AllowGet); 

            #region wcfCLient
            //JobInfo[] jobInfo = client.GetjobInfo();
            //List<JobInfo> listJobInfo = new List<JobInfo>();
            //foreach (var item in jobInfo)
            //{
            //    listJobInfo.Add(new JobInfo()
            //    {
            //        ctid = item.ctid,
            //        ExtensionData = item.ExtensionData,
            //        foid = item.foid,
            //        jauth = item.jauth,
            //        jdate = item.jdate,
            //        jid = item.jid,
            //        jleng = item.jleng,
            //        jname = item.jname,
            //        joper = item.joper,
            //        jrank = item.jrank,
            //        jstat = item.jstat,
            //        jtbatc = item.jtbatc,
            //        jtdate = item.jtdate,
            //        jtype = item.jtype,
            //        pid = item.pid,
            //        trmid = item.trmid
            //    });
            //}
            //var gridData = new { Rows = listJobInfo, Total = listJobInfo.Count };
            //return Json(gridData, JsonRequestBehavior.AllowGet); 
            #endregion
        }


        public JsonResult GetIngreInfo(int jid)
        {
            List<JobIngreForageInfo> listJobIngre = jobBll.GetJobFeedInfoByJid(jid);
            var gridData = new { Rows = listJobIngre, Total = listJobIngre.Count };
            return Json(gridData, JsonRequestBehavior.AllowGet); 


            #region wcfClient
            //JobIngreForageInfo[] jobIngreInfo = client.GetIngreForageInfo(jid);
            //List<JobIngreForageInfo> listJobIngreInfo = new List<JobIngreForageInfo>();
            //foreach (var item in jobIngreInfo)
            //{
            //    listJobIngreInfo.Add(new JobIngreForageInfo()
            //    {
            //        fid = item.fid,
            //        fname = item.fname,
            //        jid = item.jid,
            //        jiint = item.jiint,
            //        jiweig = item.jiweig
            //    });
            //}
            //var gridData = new { Rows = listJobIngreInfo, Total = listJobIngreInfo.Count };
            //return Json(gridData, JsonRequestBehavior.AllowGet); 
            #endregion
        }


        public JsonResult GetFeedList()
        {
            List<FeedInfo> FeedList = jobBll.GetFeedList();
            var gridData = new { Rows = FeedList, Total = FeedList.Count };
            return Json(gridData, JsonRequestBehavior.AllowGet); 
            #region wcfclient
            //FeedInfo[] feedInfo = client.GetFeedInfoList();
            //List<FeedInfo> listfeedInfo = new List<FeedInfo>();
            //foreach (var item in feedInfo)
            //{
            //    listfeedInfo.Add(new FeedInfo()
            //    {
            //        feid = item.feid,
            //        feleng = item.feleng,
            //        fename = item.fename,
            //        fetdate = item.fetdate,
            //        jid = item.jid,
            //        jrank = item.jrank,
            //        jstat = item.jstat,
            //        stid = item.stid,
            //        ttid = item.ttid
            //    });
            //}
            //var gridData = new { Rows = listfeedInfo, Total = listfeedInfo.Count };
            //return Json(gridData, JsonRequestBehavior.AllowGet); 
            #endregion
        }


        public JsonResult GetFeedCowList(int feid)
        {
            List<FeedCowInfo> FeedCowInfo =jobBll.GetFeedCowlistByfeid(feid);
            var gridData = new { Rows = FeedCowInfo, Total = FeedCowInfo.Count };
            return Json(gridData, JsonRequestBehavior.AllowGet); 
            #region MyRegion
            //FeedCowInfo[] feedlistCowInfo = client.GetFeedCowInfoList(feid);
            //List<FeedCowInfo> listfeedCowInfo = new List<FeedCowInfo>();
            //foreach (var item in feedlistCowInfo)
            //{
            //    listfeedCowInfo.Add(new FeedCowInfo()
            //    {
            //        csid = item.csid,
            //        csname = item.csname,
            //        flint = item.flint,
            //        flweig = item.flweig
            //    });
            //}
            //var gridData = new { Rows = listfeedCowInfo, Total = listfeedCowInfo.Count };
            //return Json(gridData, JsonRequestBehavior.AllowGet); 
            #endregion
        }

    
        public JsonResult GetAnalysis()
        {
            AnalysisBLL bll = new AnalysisBLL();
            List<object> listobj = bll.GetAnalysis();
            JavaScriptSerializer jsS = new JavaScriptSerializer();
            string result = jsS.Serialize(listobj);
            return Json(result,JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetGaochanAnalysis()
        {
            AnalysisBLL bll = new AnalysisBLL();
            List<object> listobj = bll.GetGaochanAnalysis();
            JavaScriptSerializer jsS = new JavaScriptSerializer();
            string result = jsS.Serialize(listobj);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetGannaiAnalysis()
        {
            AnalysisBLL bll = new AnalysisBLL();
            List<object> listobj = bll.GetGannaiAnalysis();
            JavaScriptSerializer jsS = new JavaScriptSerializer();
            string result = jsS.Serialize(listobj);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

    }
}