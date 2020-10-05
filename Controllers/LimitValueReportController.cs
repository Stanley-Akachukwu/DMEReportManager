using DMEWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DMEWebApp.Controllers
{
    public class LimitValueReportController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            //this method loads the index page. Note that the index page has two tabs that loads the ViewAll and AddorEdit page
            return View();
        }
        [HttpGet]
        public ActionResult ViewAll()
        {
            //this method loads the ViewAll page when ViewAll tab in Index page is clicked
            return View(GetAllReport());
        }

        IEnumerable<ReportInsRegLimitValue> GetAllReport()
        {
            //this method loads the records from database
            using (DMEEntities db = new DMEEntities())
            {
                return db.ReportInsRegLimitValues.ToList<ReportInsRegLimitValue>();
            }
        }
        [HttpGet]
        public ActionResult AddOrEdit(int? id = 0)
        {
            //this method loads the AddOrEdit page when Add New tab in Index page is clicked or when Edit button in ViewAll page is clicked
            ReportInsRegLimitValue report = new ReportInsRegLimitValue();
           
            if (id != 0)
            {
                using (DMEEntities db = new DMEEntities())
                {
                    //ReportInsRegLimitValue is the view model. If id is not zero the we retrieve the record with id
                    report = db.ReportInsRegLimitValues.Where(x => x.InstrumentEid == id.ToString()).FirstOrDefault<ReportInsRegLimitValue>();
                }
            }
            else
            {
                //ReportInsRegLimitValue   view model date time field are set to default date which is the current date time
                report.ReportInsRegLimitValueScdStartDate = DateTime.Now;
                report.ReportInsRegLimitValueScdEndDate = DateTime.Now;
                report.ReportInsRegLimitValueScdCurrentIndicator = true;
                report.LastUpdatedDateTime = DateTime.Now;
            }
            return View(report);
        }
         
        [HttpPost]
        public ActionResult AddOrEdit(ReportInsRegLimitValue reportInsRegLimitValue)
        {
            //this method  recieves the submitted data from AddOrEdit page and saves to database
            if (reportInsRegLimitValue.LastUpdatedBy ==null)
            {
                //this condition checks if LastUpdatedBy is empty. If it is true then it adds 'System Default' to avoid null exception
                reportInsRegLimitValue.LastUpdatedBy = "System Default";
            }
            try
            {
                using (DMEEntities db = new DMEEntities())
                {
                    //here we are using entity context which is DMEEntities to add data to datatbase. ReportInsRegLimitValue is the table name
                    db.ReportInsRegLimitValues.Add(reportInsRegLimitValue);
                    //the following executes to save the changes
                    db.SaveChanges();
                }
                //the following executes to return a message to the user
                return Json(new { success = true, html = GlobalClass.RenderRazorViewToString(this, "ViewAll", GetAllReport()), message = "Submitted Successfully" }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                //the following executes to handle exception in case we run into problem trying to save a record
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
            
        }
    }
}