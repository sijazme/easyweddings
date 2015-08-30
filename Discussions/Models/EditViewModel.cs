using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;


namespace Discussions.Models
{
    public class EditViewModel
    {

        private discussionsEntities db = new discussionsEntities();


        public EditViewModel()
        {
            
        }

        public EditViewModel(int? id)
        {                
             Intialize(id ?? 0);
        }

        [Required(ErrorMessage = "Date is required")]       
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        public int Id { get; set; }

        [Required(ErrorMessage = "Location is required")]
        public string Location { get; set; }

        [Required(ErrorMessage = "Outcome is required")]
        public string Outcome { get; set; }

        [Required(ErrorMessage = "Subject is required")]
        public string Subject { get; set; }

        [Required]
        public int ObserverId { get; set; }

        [Required]
        public int ParticipantId { get; set; }

        public IEnumerable<SelectListItem> Participants
        {
            get
            {
                var list = new List<SelectListItem>();
                foreach (var e in db.Employees)
                {
                    
                    var item = new SelectListItem { Text = string.Format("{0} {1}", e.FirstName, e.LastName), Value = e.Id.ToString() };
                    item.Selected = e.Id == ParticipantId;
                    list.Add(item);
                }

                return list;
            }
        }

        public IEnumerable<SelectListItem> Observers
        {
            get
            {
                var list = new List<SelectListItem>();
                foreach (var e in db.Employees)
                {
                    var item = new SelectListItem { Text = string.Format("{0} {1}", e.FirstName, e.LastName), Value = e.Id.ToString() };
                    item.Selected = e.Id == ObserverId;
                    list.Add(item);
                }

                return list;
            }
        }

        private void Intialize(int? id)
        {
            if (id > 0)
            {

                var d = db.Discussions.Find(id);

                Date = d.Date;
                Id = d.Id;
                Location = d.Location;
                Outcome = d.Outcome;
                Subject = d.Subject;


                var eds = db.EmployeeDiscussions.Where(x => x.DiscussionId == Id).ToList();

                foreach (var e in eds)
                {
                    if (e.IsObserver)
                    {
                        ObserverId = e.Employee.Id;
                    }
                    else
                    {
                        ParticipantId = e.Employee.Id;
                    }
                }
            }
        }

       
    }
}
