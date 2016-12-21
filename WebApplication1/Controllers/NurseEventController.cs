using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication1.DAL;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Authorize]
    public class NurseEventController : Controller
    {
        private NurseEventContext db;

        public NurseEventController()
        {
            db = new NurseEventContext();
            db.Database.Log = s => System.Diagnostics.Trace.WriteLine(s);
        }

        public ActionResult Index()
        {
            return Index("", "", true, true);
        }

        [ActionName("Index1")]
        public ActionResult Index(string searchfromdate = "", string searchtodate = "", bool searchyettohandle = true, bool searchandled = true)
        {
            var checkExpression = PredicateBuilder.True<NurseEvent>();

            DateTime fromtime;
            if (DateTime.TryParse(searchfromdate + " 00:00:00", out fromtime))
            {
                if ((fromtime > DateTime.Now.AddDays(-1)) || (fromtime < DateTime.Now.AddYears(-2)))
                {
                    fromtime = Convert.ToDateTime(DateTime.Now.AddYears(-1).ToString("yyyy-MM-dd") + " 00:00:00");
                }
            }
            else
            {
                fromtime = Convert.ToDateTime(DateTime.Now.AddYears(-1).ToString("yyyy-MM-dd") + " 00:00:00");
            }
            checkExpression = checkExpression.And(s => s.event_happen_time > fromtime);

            DateTime totime;
            if (DateTime.TryParse(searchtodate + " 00:00:00", out totime))
            {
                if ((totime > DateTime.Now.AddDays(-1)) || (totime < DateTime.Now.AddYears(-2)))
                {
                    totime = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00");
                }
            }
            else
            {
                totime = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00");
            }
            checkExpression = checkExpression.And(s => s.event_happen_time < totime);
            

            if (User.IsInRole("领导组"))
            {

            }
            else if (User.IsInRole("护士组") || User.IsInRole("护士长组"))
            {
                string dept_name = User.Identity.Name.Split(new char[] { ',' })[2].TrimEnd("护理站".ToCharArray());
                checkExpression = checkExpression.And(s => s.dept_name == dept_name);
            }

            if (searchandled)
            {
                checkExpression = checkExpression.And(s => s.handled_by_headnurse == "dept_name");
            }
            if (searchyettohandle)
            {
                checkExpression = checkExpression.And(s => s.handled_by_headnurse == "dept_name");
            }
            //var tmp = db.NurseEvents.Where(checkExpression);
            //string sql = ((System.Data.Entity.Core.Objects.ObjectQuery)tmp).ToTraceString();
            return View("Index",db.NurseEvents.Where(checkExpression).ToList());

        }

        // GET: NurseEvent/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NurseEvent nurseEvent = db.NurseEvents.Find(id);
            if (nurseEvent == null)
            {
                return HttpNotFound();
            }
            return View(nurseEvent);
        }

        // GET:
        public ActionResult PartialDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NurseEvent nurseEvent = db.NurseEvents.Find(id);
            if (nurseEvent == null)
            {
                return HttpNotFound();
            }
            return PartialView("_PartialDetails", nurseEvent);
        }

        // GET: NurseEvent/Create
        public ActionResult Create()
        {
            NurseEvent nurseEvent = new NurseEvent {event_happen_time=DateTime.Now, reporter_id = User.Identity.Name.Split(new char[] { ',' })[0], reporter_name = User.Identity.Name.Split(new char[] { ',' })[1] };
            return View(nurseEvent);
        }

        // POST: NurseEvent/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,patient_name,dept_name,bed_no,patient_age,patient_sex,charge_type,diagnosis,inp_no,admission_date,event_happen_time,event_info,event_level,event_service,event_name,event_fact,event_relevant_factor,event_patient_status,event_harm,event_harm_grade,event_location,event_concerned_staff1,event_concerned_staff2,event_concerned_staff3,event_treatment,event_treatment_after,reporter_name,reporter_id,handled_by_headnurse,handled_by_leader")] NurseEvent nurseEvent)
        {
            if (ModelState.IsValid)
            {
                db.NurseEvents.Add(nurseEvent);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(nurseEvent);
        }

        // GET: NurseEvent/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NurseEvent nurseEvent = db.NurseEvents.Find(id);
            if (nurseEvent == null)
            {
                return HttpNotFound();
            }
            return View(nurseEvent);
        }

        // POST: NurseEvent/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,patient_name,dept_name,bed_no,patient_age,patient_sex,charge_type,diagnosis,inp_no,admission_date,event_happen_time,event_info,event_level,event_service,event_name,event_fact,event_relevant_factor,event_patient_status,event_harm,event_harm_grade,event_location,event_concerned_staff1,event_concerned_staff2,event_concerned_staff3,event_treatment,event_treatment_after,reporter_name,reporter_id,handled_by_headnurse,handled_by_leader")] NurseEvent nurseEvent)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nurseEvent).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(nurseEvent);
        }

        // GET: NurseEvent/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NurseEvent nurseEvent = db.NurseEvents.Find(id);
            if (nurseEvent == null)
            {
                return HttpNotFound();
            }
            return View(nurseEvent);
        }

        // POST: NurseEvent/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            NurseEvent nurseEvent = db.NurseEvents.Find(id);
            db.NurseEvents.Remove(nurseEvent);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
