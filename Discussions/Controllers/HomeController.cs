using Discussions.Models;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace Discussions.Controllers
{
    public class HomeController : Controller
    {
        private discussionsEntities db = new discussionsEntities();

        public ActionResult Index()
        {
            return View(new IndexViewModel());
        }

        public ActionResult Edit(int? id)
        {
            var evm = id == null || id <= 0 ? new EditViewModel(1) : new EditViewModel(id);
            return View(evm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditViewModel evm)
        {
            if (ModelState.IsValid)
            {
                var d = db.Discussions.Find(evm.Id);
                d.Date = evm.Date;
                d.Location = evm.Location;
                d.Outcome = evm.Outcome;
                d.Subject = evm.Subject;

               var eds = db.EmployeeDiscussions.Where(x => x.DiscussionId == evm.Id);

               if (eds == null || eds.Count() == 0)
               {
                   var ed1 = new EmployeeDiscussion();
                   ed1.EmployeeId = evm.ObserverId;
                   ed1.IsObserver = true;
                   ed1.DiscussionId = evm.Id;

                   var ed2 = new EmployeeDiscussion();
                   ed2.EmployeeId = evm.ParticipantId;
                   ed2.IsObserver = false;
                   ed2.DiscussionId = evm.Id;

                   db.Entry(ed1).State = EntityState.Added;
                   db.Entry(ed2).State = EntityState.Added;
               }

               else
               {
                   foreach (var ed in eds)
                   {
                       if (ed.IsObserver)
                       {

                           ed.EmployeeId = evm.ObserverId;
                       }

                       else
                       {
                           ed.EmployeeId = evm.ParticipantId;
                       }

                       db.Entry(ed).State = EntityState.Modified;
                   }
               }

                try
                {
                    db.Entry(d).State = EntityState.Modified;
                  
                    db.SaveChanges();
          
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {                   
                    ModelState.AddModelError(ex.Message, "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }

            return View(evm);
        }

        // ----------------------------------------------------

        public ActionResult Create()
        {
            return View(new CreateViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateViewModel evm)
        {
            if (ModelState.IsValid)
            {
                var d = new Discussion();
                d.Date = evm.Date;
                d.Location = evm.Location;
                d.Outcome = evm.Outcome;
                d.Subject = evm.Subject;

                var ed1 = new EmployeeDiscussion();
                ed1.EmployeeId = evm.ObserverId;
                ed1.IsObserver = true;

                var ed2 = new EmployeeDiscussion();
                ed2.EmployeeId = evm.ParticipantId;
                ed2.IsObserver = false;

                try
                {

                    
                    db.Entry(d).State = EntityState.Added;
                    db.SaveChanges();

                    db.Entry(ed1).State = EntityState.Added;
                    db.Entry(ed2).State = EntityState.Added;

                    ed1.DiscussionId = d.Id;
                    ed2.DiscussionId = d.Id;

                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(ex.Message, "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }                    
            }

            return View(evm);
        }
    }
}